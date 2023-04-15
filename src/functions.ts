import { MongoClient, ObjectId } from "mongodb";
import { _apiQuery, _customer, _multipleCustomers } from "./types";
const client = new MongoClient(process.env.MONGODB_KEY!);
const customerCollection = client
  .db("mock-customers-PROD")
  .collection<_customer>("customers");

function ReturnRandomCustomerArray(
  limit: number,
  customers: _customer[]
): _multipleCustomers | null {
  try {
    /* 
      randomly return the number of documents based on
      limit query
      */
    let rArray: _customer[] = [];
    let rIndexesUsed: number[] = [];
    while (rArray.length !== limit) {
      const rIndex: number = Math.floor(Math.random() * customers.length);
      if (rIndexesUsed.indexOf(rIndex) === -1) {
        rArray.push(customers[rIndex]);
        rIndexesUsed.push(rIndex);
      } else {
        continue;
      }
    }
    return {
      num_of_customers: limit,
      customer_data: rArray,
    };
  } catch (e) {
    console.log(e);
    console.log("error while returning random customers array");
    return null;
  }
}

export async function ConnectToDb(): Promise<true | null> {
  try {
    await client.connect();
    console.log("successfully connected to database!");
    return true;
  } catch (e) {
    console.log(e);
    console.log("error while connecting to database");
    return null;
  }
}

export async function FetchRandomCustomer(): Promise<any | null> {
  try {
    const data = await customerCollection
      .aggregate([{ $sample: { size: 1 } }])
      .toArray();

    if (data.length > 0) {
      const x = { ...data[0] };

      delete x._id;
      return x;
    } else {
      console.log("no data stored ");
      return null;
    }
  } catch (e) {
    console.log(e);
    console.log("error while fetching random customer");
    return null;
  }
}

export async function QueryUrl(
  query: _apiQuery
): Promise<_multipleCustomers | _customer | null> {
  /* 
  check all url queries before limit query, check to see
  if limit query is attached to all queries 
  */

  try {
    //?PRODUCT
    if (query.product !== undefined) {
      //return single random record with matching product category
      const data = await customerCollection
        .find(
          { product_category: query.product },
          { projection: { _id: false } }
        )
        .toArray();
      const rIndex: number = Math.floor(Math.random() * data.length);
      return data[rIndex];
    }
    //?LIMIT
    if (query.limit !== undefined) {
      //ERRORS
      if (Number(query.limit) < 2 || Number(query.limit) > 1000) {
        return null;
      }
      let data = await customerCollection
        .find({}, { projection: { _id: false } })
        .toArray();

      return ReturnRandomCustomerArray(Number(query.limit), data);
    }

    //no query present, return null
    return null;
  } catch (e) {
    console.log(e);
    console.log("could not fetch data with current url query");
    return null;
  }
}
