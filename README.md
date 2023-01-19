# Database with 50,000 fake customer records

This API makes it easy to fetch random mock customer data to use in your next project. There is one single endpoint to hit to get data **/api**. The attributes of the customers returned include:
- ID
- First name
- Last name
- Email
- Phone number
- Address
- Credit card number (fake of course)
- Order total

There are 2 url queries that can be used when fetching customer data, _?id_ and _?limit_. 

- **?id** will retrieve the customer with the supplied id. Will return a single record.
- **?limit** returns more than 1 record at a time (up to 1,000) with a single request.

Feel free to use this application to help jumpstart your next project with fake data needed for testing or any other purposes.