# MotoHub Professional Demo

Responsive multi-page motorcycle service website with a dark graphite / red visual system. Each screen has its own HTML file, CSS file, and JavaScript entry file. Shared styling and app logic live in `css/common.css`, `js/common.js`, `js/auth.js`, and `js/data.js`.

## Run locally
1. Extract the ZIP.
2. Open the `MotoHub_Professional` folder in VS Code.
3. Install/use Live Server and open `index.html`.
4. The splash screen automatically proceeds to Login.

## Demo admin
- Email: `admin@motohub.com`
- Password: `Admin123!`

## Included screens
Splash, Login, Sign Up, Forgot Password, Home, Services, Parts, Cart, Booking, Booking Success, Booking Details, My Bookings, My Garage, Add Motorcycle, Profile, Notifications, Account Settings, Admin dashboard.

## Functionality
- Sign up and login with separate user/admin role routing.
- Browser-local user/session storage.
- Service selection and booking creation, detail view, cancellation, status updates.
- Motorcycle garage add/remove.
- Parts search/category, cart, demo checkout.
- Profile editing, notifications, local preferences.
- Admin booking status management, rider directory, service price editing.

## Important limitation
This is a front-end demo using browser `localStorage`; it is not secure production authentication and does not provide real email delivery, payment processing, server-side database, or multi-device synchronization. Do not use real passwords. For production, connect a backend and database.


## Recent frontend updates
- Login page now includes a Show/Hide password control.
- Cart checkout offers GCash, Cash, and PayMaya as payment-method selections.
- Confirming a parts order creates a local pending order visible in My Bookings with each item, quantity, selected payment method, and calculated total.
- This remains a front-end demonstration: no real GCash/PayMaya payment is processed and the data is stored in this browser's localStorage.
