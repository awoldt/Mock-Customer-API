# Database with 50,000 fake customer records

This API makes it easy to fetch random mock customer data to use in your next project. There is one single endpoint to hit to get data **/api**. The attributes of each customer are:
- ID
- First name
- Last name
- Email
- Phone number
- Address
- Credit card number (fake of course)
- Order total

There are 2 url queries that can be used when fetching customers, _id_ and _limit_. 

- **id** will retrieve the customer with the supplied id (id is the primary key in the database). This will return a single record.
    - Example: _?id=236_ would return the customer record with the id of 236.  
- **limit** returns more than 1 record at a time (up to 1,000) with a single request.
    - Example: _?limit=750_ would return 750 random records from the database.

You can also download a randomly generated JSON file of up to 1,000 customers.