// Next.js API route support: https://nextjs.org/docs/api-routes/introduction
import type { NextApiRequest, NextApiResponse } from "next";
import { ConnectToDb, FetchRandomCustomer, QueryUrl } from "@/functions";
import { _multipleCustomers, _customer } from "@/types";

type Data =
  | null
  | _customer
  | _multipleCustomers
  | 404
  | "limit_error"
  | string;

const dbConnection = ConnectToDb();
let handler;

if (await dbConnection) {
  handler = async function handler(
    req: NextApiRequest,
    res: NextApiResponse<Data>
  ) {
    //whenver there is url query present
    if (req.query.product !== undefined || req.query.limit !== undefined) {
      const data = await QueryUrl(req.query);

      //404
      if (data === 404) {
        res.status(404).send("No customers with current query could be found");
      }
      //400
      else if (data === "limit_error") {
        res
          .status(400)
          .send("Limit query cannot be less than 2 or greater than 1000");
      }
      //500
      else if (data === null) {
        res.status(500).send("Error while fetching customer data");
      }
      //200
      else {
        res.status(200).json(data);
      }
    }
    //no url query, return single random customer
    else {
      res.status(200).json(await FetchRandomCustomer());
    }
  };
} else {
  handler = function handler(req: NextApiRequest, res: NextApiResponse<Data>) {
    res.status(500).json(null);
  };
}

export default handler;
