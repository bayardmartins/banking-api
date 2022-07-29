### Customer Endpoints
  
Base Endpoint: ~/v1/api/Customer  
All endpoints need Bearer token (see Login doc)  
  
## GetAll
Get All Customer  
GET /  
Params: none  
Return: All Customer  
Response: 200 -> return all customer  
  
## GetById
Get Customer by Id  
GET /1  
Params: Id  
Return: Customer  
Response: 200 -> return customer  
          404 -> return not found error message  
  
## Post
Create Customer  
POST /  
Parms: query: CustomerId  
body:  
{  
    "name": "name test",  
    "password": "strongpassword",  
    "nuDocument": "12345"  
}  
Return: Return messagge with created customer Id  
Response: 200 -> Return messagge with created customer Id  
          422 -> Return error message  
  
## Put
Update Customer  
PUT /1  
Parms: query: CustomerId  
body:  
{  
    "id": 1  
    "name": "name test",  
    "password": "strongpassword",  
    "nuDocument": "12345"  
}  
Return: Return messagge with created customer Id  
Response: 200 -> Return messagge with created customer Id  
          422 -> Return error message  
          404 -> Return not found message  
  
## Delete
Delete Customer  
DELETE /1  
Parms: Id  
Return: Return messagge with deleted customer Id  
Response: 200 -> Return messagge with deleted customer Id  
          422 -> Return error message  
          404 -> Return not found message  
  
## Inactivate
Inactivate Customer  
POST /1/Inactivate  
Params: Id  
Return: Return message with inativated customer Id  
Response: 200 -> return messagge with deleted customer Id  
          422 -> Return error message  
          404 -> return not found error message  
  