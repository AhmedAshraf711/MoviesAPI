# Movies API
## Table Of Content
* [project notes](https://github.com/AhmedAshraf711/MoviesAPI?tab=readme-ov-file#project-notes)
*  [Tools](https://github.com/AhmedAshraf711/MoviesAPI?tab=readme-ov-file#tools)
* [How it Work](https://github.com/AhmedAshraf711/MoviesAPI/blob/master/README.md#how-it-work)


### project notes
This Project Consists Of Four Operations for two Controllers **Movies** and **Genres** 
this operation is :
- Create 
- Update 
- Delete
- Detailes

, two Controllers **Roles** for manage roles in API and **Account** for manage registration and login  
And used some concepts to build this project by clear way such as **DependancyInjection** and **Automapper** and **Data Transfer Object (DTO)**

**Created By RESTAPI**

 operations Such as :
 
 - **Create** : responsible for adding movies and geners to project .
 
 - **Edit**   : responsible for updating movies on project .
 
 - **Delete** : responsible for Delete movies from project.

**only Admins can apply them** ,  also endpoint of  **Detailes**  this endpoint  responsible for **showing**  movies in movies controller  and **showing** genres in genres controller, admins can execute it also.

 Users can display page of **Detailes** Only.
   
Applay **Identity :**
- Project also contains a **Register** and **Login**.
- Add two roles **Admin** , **User**.
- Applay **Authentication** by **JWT** to ensure that any user want to access to endpoiints have a **token**.
- Applay **Authorization** to ensure that user have a **Authoriz** to access to a number of endpoints or all endpoints ,role of each user determines this thing.

## Tools
- ASP.NET6
- EntityFrameWorkCore
- LINQ

 ## How it Work 
 - clone this repo : https://github.com/AhmedAshraf711/Movies.git


> [!IMPORTANT]
>  ### Installation
>  - Microsoft.EntityFrameworkCore 6.0.26
>  - Microsoft.EntityFrameworkCore.SqlServer 6.0.26
>  - Microsoft.EntityFrameworkCore.Tools 6.0.26
>  - Microsoft.AspNetCore.Identity.EntityFrameworkCore 6.0.26
>  - Microsoft.AspNetCore.Authentication.JwtBearer 6.0.26
>  - Swashbuckle.AspNetCore 6.5.0
>  - AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.1


