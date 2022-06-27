# Express Tutorial

Express server that has a local sqlite3 database [SQLite3](https://expressjs.com/en/guide/database-integration.html#sqlite)

The server should have the following resources:

| Resource      | Type     | Description             |
| ------------- | -------- | ----------------------- |
| `/asset`      | `POST`   | Creates an asset        |
| `/asset/{id}` | `GET`    | Gets an asset by id     |
| `/asset/{id}` | `DELETE` | Deletes an asset by id  |
| `/asset/{id}` | `PATCH`  | Modifies an asset by id |
| `/assets`     | `GET`    | Gets all assets         |

Asset:

- `int:key` id
- `int` type (0 will be a drone)
- `text` name (could be something like X1-001337 to be drone UID 1337)
- `text owner` (like Verge, Inc.)
  
All transactions are as JSON strings. Therefore, if you use Postman or your browser you should be able to make a request to http://localhost:3000/assets and return a JSON object with all assets. Another example would be http://localhost:3000/asset/0 to get the first asset as a JSON object.

You can fill the database up on initialization. But you should also be able to use Postman to POST a new asset in the local database.
