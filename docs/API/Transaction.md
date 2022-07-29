### Transaction Endpoints
  
Base Endpoint: ~/v1/api/Transaction  
All endpoints need Bearer token (see Login doc)  
  
## Deposit
Execute Deposit  
POST /Deposit  
Parms: body:  
{  
    "destinyAccountId": 1,  
    "value": 100.00  
}  
Return: Return messagge with created transaction Id  
Response: 200 -> Return messagge with created transaction Id  
          400 -> Return validation error message  
          422 -> Return account error message  
          422 -> Return transaction error  message  
  
## Withdraw
Execute Withdraw  
POST /Withdraw  
Parms: body:  
{  
    "destinyAccountId": 1,  
    "value": 100.00  
}  
Return: Return messagge with created transaction Id  
Response: 200 -> Return messagge with created transaction Id  
          400 -> Return validation error message  
          422 -> Return account error message  
          422 -> Return transaction error  message  
  
## Transaction
Execute Transaction  
POST /Transaction  
Parms: body:  
{  
    "originAccountId": 2,  
    "destinyAccountId": 1,  
    "value": 100.00  
}  
Return: Return messagge with created transaction Id  
Response: 200 -> Return messagge with created transaction Id  
          400 -> Return validation error message  
          422 -> Return account error message  
          422 -> Return transaction error  message  
  