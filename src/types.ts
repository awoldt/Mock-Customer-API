import { ObjectId } from "mongodb";

export interface _customer {
  _id?: ObjectId;
  first_name: string;
  last_name: string;
  gender: "Male" | "Female";
  email: string;
  address: string;
  phone_number: string;
  card_number: string;
  order_total: string;
  order_date: string;
  product_category: string;
}

export interface _apiQuery {
  product?: string;
  limit?: number;
}

export interface _multipleCustomers {
  num_of_customers: number;
  customer_data: _customer[];
}
