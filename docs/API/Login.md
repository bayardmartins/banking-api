### Login Endpoints
  
Base Endpoint: ~/v1/api/Login  
All endpoints don't need Bearer token  
  
## Login
Login User  
POST /auth  
Parms: body:  
{  
    "login": "userLogin",  
    "password": "strongPassword"  
}  
Return: Return messagge with access token (must use this token as Bearer token for all   other controllers endpoints)  
Response: 200 -> Return user and token  
          400 -> Return error message (wrong login or password)  
  
## Register
Register User  
POST /register  
Parms: body:  
{  
    "login": "userLogin",  
    "password": "strongPassword"  
}  
Return: Return messagge with registred user Id  
Response: 200 -> Return registred user Id  
          400 -> Return error message  
  