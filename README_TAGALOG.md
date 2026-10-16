# MotoHub — Frontend + REST API source

**Project title:** MotoHub: Motorcycle Service, Maintenance and Parts Management System

Kasama sa ZIP ang 62-file frontend prototype at bagong ASP.NET Core Web API source. Ang API ay gumagamit ng SQL Server relational database, JSON REST endpoints, password hashing, JWT bearer authentication, role-based authorization, validation, error responses, at external Open-Meteo weather API integration.

> **Mahalagang katapatan:** Hindi na-build o na-run dito ang .NET API dahil walang `dotnet` SDK sa environment na ginamit sa pagbuo. Hindi rin awtomatikong napalitan ang lahat ng frontend demo/localStorage actions para tumawag sa API. Nasa `Frontend/js/api-client.js` ang API helper na gagamitin sa migration ng bawat page. Huwag ipresenta na fully integrated/tested hangga't hindi mo ito napapatakbo at nasusubukan sa PC mo.

## 1. Mga kailangan
- Windows 10/11
- .NET 8 SDK
- SQL Server LocalDB (kasama kadalasan sa Visual Studio) o SQL Server instance
- Visual Studio 2022 o VS Code
- Live Server extension para sa frontend

## 2. Patakbuhin ang API
1. I-install ang .NET 8 SDK at SQL Server LocalDB.
2. Buksan ang `MotoHubAPI` folder sa terminal.
3. Sa `appsettings.json`, palitan ang `Jwt:Key` ng random secret na hindi bababa sa 32 characters. Huwag i-upload ang real secret sa GitHub.
4. Kung hindi LocalDB ang gamit, palitan ang `ConnectionStrings:MotoHubDb` ayon sa SQL Server instance mo.
5. Patakbuhin:
   ```powershell
   dotnet restore
   dotnet run --urls http://localhost:5000
   ```
6. Buksan ang `http://localhost:5000/swagger` para makita at subukan ang endpoints.
7. Sa unang start, gagawa ang API ng database/tables at sample service/parts. Demo admin account:
   - Email: `admin@motohub.local`
   - Password: `ChangeMe123!`
   - I-set ang environment variable na `MOTOHUB_ADMIN_PASSWORD` bago first run para palitan ang demo password.

Kung `EnsureCreated` ang ginamit para sa unang database, huwag basta magdagdag ng EF migrations sa parehong database. Para sa production/graded final build, palitan ito ng formal EF Core migrations.

## 3. Frontend
1. Buksan ang `Frontend` folder sa VS Code.
2. I-run ang `index.html` gamit ang Live Server. Default CORS origins ay localhost/127.0.0.1 ports 5500 at 5501.
3. API helper: `Frontend/js/api-client.js`. Base URL: `http://localhost:5000/api`.
4. Existing screens retain demo/localStorage behavior until their page scripts are changed to call `MotoHubAPI`. Halimbawa, login page must call `MotoHubAPI.login(email,password)` and then route using the returned `user.role`.

## 4. Endpoints
| Method | Endpoint | Access | Gamit |
|---|---|---|---|
| POST | `/api/auth/register` | Public | Customer registration |
| POST | `/api/auth/login` | Public | Login, returns JWT |
| GET | `/api/services` | Public | List active services |
| POST/PUT/DELETE | `/api/services` | Admin | CRUD services (delete is soft-delete) |
| GET | `/api/parts` | Public | List active parts |
| POST/PUT/DELETE | `/api/parts` | Admin | CRUD parts (delete is soft-delete) |
| GET/POST | `/api/motorcycles` | Authenticated | View/add user's motorcycles |
| PUT/DELETE | `/api/motorcycles/{id}` | Owner/Admin | Update/delete motorcycle |
| GET/POST | `/api/bookings` | Authenticated | List own bookings/create booking |
| PATCH | `/api/bookings/{id}/status` | Admin | Update booking status |
| GET | `/api/weather?latitude=8.35&longitude=124.87` | Public | External Open-Meteo weather data |

## 5. Database entities
- `Users`: account details, hashed password, role
- `Motorcycles`: owner, make/model/year/plate
- `Services`: service catalog and price
- `Parts`: inventory and stock
- `Bookings`: user, motorcycle, service, schedule, status

## 6. Test checklist
- Register a customer; try registering the same email twice (expect conflict).
- Log in with wrong password (expect 401); log in correctly and copy JWT from response.
- Test public GET services/parts.
- Test admin POST/PUT/DELETE endpoints with `Authorization: Bearer <token>`.
- Test motorcycle CRUD and verify one customer cannot access another customer's motorcycle.
- Create a future booking; test past date rejection and invalid status rejection.
- Test `/api/weather`; external API requires internet access.
- Capture actual Swagger/Postman results and document any errors before final submission.

## 7. Security notes
- The demo admin password is only for local development. Set `MOTOHUB_ADMIN_PASSWORD` before the first run and change credentials immediately.
- Keep JWT key and database credentials out of public repositories.
- Use HTTPS and a proper secret manager in deployment.
- Configure allowed CORS origins to the exact frontend origin you use.
