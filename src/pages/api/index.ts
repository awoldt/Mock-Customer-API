// Next.js API route support: https://nextjs.org/docs/api-routes/introduction
import type { NextApiRequest, NextApiResponse } from "next";
import { ConnectToDb, FetchRandomCustomer, QueryUrl } from "@/functions";
import { _multipleCustomers, _customer } from "@/types";

type Data = null | _customer | _multipleCustomers | undefined;

const dbConnection = ConnectToDb();
let handler;

if (await dbConnection) {
  handler = async function handler(
    req: NextApiRequest,
    res: NextApiResponse<Data>
  ) {
    //whenver there is url query present
    if (req.query.product !== undefined || req.query.limit !== undefined) {
      res.status(200).json(await QueryUrl(req.query));
    } else {
      res.status(200).json(await FetchRandomCustomer());
    }
  };
} else {
  handler = function handler(req: NextApiRequest, res: NextApiResponse<Data>) {
    res.status(500).json(null);
  };
}

export default handler;
