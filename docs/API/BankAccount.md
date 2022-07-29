### Bank Account Endpoints
  
Base Endpoint: ~/v1/api/BankAccount  
All endpoints need Bearer token (see Login doc)  
  
## GetAll
Get All Bank Accounts  
GET /  
Params: none  
Return: All Bank Accounts  
Response: 200 -> return all bank accounts  
  
## GetById
Get Bank Account by Id  
GET /1  
Params: Id  
Return: Bank Account  
Response: 200 -> return bank account  
          404 -> return not found error message  
  
## Post
Create Bank Account  
POST /1  
Parms: query: CustomerId  
body:  
{  
   "accountNumber": "1234",  
   "password": "strongpassword",  
   "balance": 100.00,  
}  
Return: Return messagge with created account Id  
Response: 200 -> Return messagge with created account Id  
          422 -> Return error message  
  
## Put
Update Bank Account  
PUT /1  
Parms: query: CustomerId  
body:  
{  
   "accountNumber": "1234",  
   "password": "strongpassword",  
   "balance": 100.00,  
}  
Return: Return messagge with created account Id  
Response: 200 -> Return messagge with created account Id  
          422 -> Return error message  
          404 -> Return not found message  
  
## Delete
Delete Bank Account  
DELETE /1  
Parms: Id  
Return: Return messagge with deleted account Id  
Response: 200 -> Return messagge with deleted account Id  
          422 -> Return error message  
          404 -> Return not found message  
  
## Balance
Get Bank Account Balance
GET /1/Balance
Parms: Id
Return: Return Account Balance
Response: 200 -> Return Account Balance
          422 -> Return error message
          404 -> Return not found message

## Statement
Get Statement
GET /1/Statement
Parms: query: Id
body:
{
    "month" : 1
}
Return: All transaction from the given month
Response: 200 -> Return transaction list
          400 -> Return error message

## GetByCustomerId
Get all bank accounts from given customer
GET /Customer/1
Parms: id
Return: Return all customer's bank accounts
Response: 200 -> Return all customer's bank accounts

## Inactivate
Inactivate BankAccount
POST /1/Inactivate
Params: Id
Return: Return message with inativated customer Id
Response: 200 -> return messagge with deleted customer Id
          422 -> Return error message
          404 -> return not found error message
