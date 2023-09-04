# Avaliação de Atendimento CSAT5

O CSAT 5 consiste em uma iniciativa da XP para que considerar como promotor
apenas a nota 5 e como detrator as notas 1, 2 e 3.


## Swagger Page for Routes Testing

`[Swagger](http://localhost:4870/swagger/index.html)`

## Usage

The API consists of 6 Routes, that allow the user to interact with CSAT

CSAT Model
`{
    “id:” “guid”,
    “score”: “int”,
    “comment”: “string”,
    “problemSolved”: “bool”,
    “attendantEmail”: “string”,
    "createdAt": "datetime"
}`

## Routes

Base URL:
`[server_path:port]/api/CSAT/[routes][params]`

### Create CSAT [POST]

`/createCSAT`

Request Body Example (CSAT.Object - CSAT)

`{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "score": 5,
  "comment": "Excelente!",
  "problemSolved": true,
  "email": "eu@mesmo.com.",
  "timeStamp": "2023-07-31T13:11:05.601Z"
}
`

### Consult CSAT [GET]

`/getCSAT?id=[CSAT_id]`


### Update Comment [POST]

`/updateCSATComment?id=[CSAT_id]`

Request Body Example (CSAT.Comment - string)

`{
  "Serviço Excelente!"
}
`

### Update FCR(Problem Solved) [POST]

`/updateCSATFCR?id=[CSAT_id]`

Request Body Example (CSAT.ProblemSolved - bool)

`{
    true
}
`

### List CSATs [GET]

`/getAllCSAT?score=[{int}CSAT_score]&fcr[{bool}CSAT_problemSolver]&email=[CSAT_email]`

### Summary CSATs Score [POST]

`/getCSATSummary?email=[CSAT_email]`

Request Body Example (CSAT.Timestamp - DateTime)

`{
  "2023-07-31T13:11:05.601Z"
}
`

### Stack & Libs/Dependencies (NuGet)

- MongoDB (2.20 Driver)
- Swashbuckle (6.5.0 - Tooling for AspNetCore with Swagger)
- MSTest (Testing - Std Microsoft)
- Moq (Testing - Mock Services and Data)
