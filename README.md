
# Pool Game API

This is the supposed backend for a personal mobile app that would track the stats of my pool games with my friends. The api should be responsible for creating and keeping the records into a database.


## API Endpoints
Those should be the endpoints for the functions the api has. They should be publicly available. Hopefully they still work at the time of reading. The addres of all endpints is poolgameapi.space
### Base URL
All API requests should be made to the following base URL:
`https://poolgameapi.space`

**Example:**

To get all recorded games, we can use:
 
`https://poolgameapi.space/api/GameRecords/All`

**note: all endpoints require to sent a bearer token alongside the https request**

The token is not needed for the creation of an account and can be issued by using the ```/Users``` endpoint 
#### Get Authentication Token

```
  GET /Users
```

| Parameter | Type   | Required | Description                |
| :-------- | :-------  |:--| :------------------------- |
| `Username` | `string` |yes|The username of the already registered account  |
| `Password` | `string` |yes|The password of the same account |

##### Example Request
```
GET /Users?username=123&password=123
```

##### Example response
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9
.eyJsb2dnZWRJbkFzIjoiYWRtaW4iLCJpYXQiOjE0MjI3Nzk2Mzh9
.gzSraSYS8EXBxLN _oWnFSRgCzcmJmMjLiuyu5CSpyHI
```
It is a simple string containing the token

#### Create an account

```
  POST /Users
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `Username` | `string` |The username the account would use  |
| `Password` | `string` |The password for the account |


**!currently there is no response if the username is already used!**
### How the records of a game looks like
All records of a game contain:
- **gameId** - The id of the played game. if two players played in the same game they should have the same gameid in their records.
- **player** - The player that played the game and whose stats we see.
- **result** - Self-explanatory. Did the player lose/win/draw.
- **shotsMade** - The amount of shots the player successfully made durig the game.
- **shotsAttempted** - The amount of total shots the player made, including successful and unsuccessful, during the game.
- **handball** - The amount of times the player could move the white ball or in other words had ball in hand.
- **fouls** - How many fouls the player commited.
- **bestStreak** - Longest streak of successfully made shots.

All the endpints for game records contain those fields to a specific extend.


#### Get the played games of a specific user

```
  GET /api/GameRecords/{username}
```
##### Example Request
```
GET /api/GameRecords/Rishaviq
```

##### Example response
```json
[{
    "gameId": "6",
    "player": "Rishaviq",
    "result": "lose",
    "shotsMade": 5,
    "shotAttempted": 12,
    "handball": 2,
    "fouls": 3,
    "bestStreak": 3
}]
```
 Returns a list of the recorded games. 

#### Get all of the played games 

```
  GET /api/GameRecords/All
```
##### Example response
```json
[{
    "gameId": "6",
    "player": "username",
    "result": "lose",
    "shotsMade": 5,
    "shotAttempted": 12,
    "handball": 2,
    "fouls": 3,
    "bestStreak": 3
}]
```
 Returns a list of all games on record, no matter the players involved. 


#### Create a record of a game

```
  POST /api/GameRecords
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `gameId` | `string` |Id of the game |
| `player` | `string` |Name of the player |
| `result` | `string` |The result of the game  |
| `shotsMade` | `int` |Shots successfully made |
| `shotAttempted` | `int` |Shots attempted this game  |
| `handball` | `int` |Times player could move white ball |
| `fouls` | `int` |Fouls commited |
| `bestStreak` | `int` |Longest streak of successfully made shots  |
