# AddressBook

## Project setup

1. Setup the database
2. Setup database connection string in `AddressBook.Application > appsettings.Development.json `
3. Run the project
4. Use Postman or a similar tool to test the API

## Database setup
Database script can be found in `AddressBook.Repository > Database Scripts`

1. Create a database using the `CreateDatabase.sql`. You can change the database name in there
2. Create table(s) in the database using the `CreateTables.sql`.
3. You can populate the table(s) with sample data from `SampleData.sql`

## API reference

- `GET /api/AddressBook/{id}`
- `GET /api/AddressBook/{name}/{address}/{pageNumber}`
- `GET /api/AddressBook/page/{pageNumber}`
- `POST /api/AddressBook`
- `PUT /api/AddressBook/{id}`
- `DELETE /api/AddressBook/{id}`

POST and PUT methods require a Contact entry object in their bodies
```
{
   "id": 1, // only used in PUT
   "name":"Zlatko Hrastić",
   "dateOfBirth":"1994-08-16T00:00:00",
   "address":"Augusta Piazze 2",
   "telephoneNumbers":[
      "013833654",
      "091852654"
   ]
}
```

