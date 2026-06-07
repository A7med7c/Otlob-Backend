# FRONTEND API CONTRACT

This document describes the exact backend API contract expected by the Angular frontend. Use this to implement or adapt an ASP.NET Core Web API.

Base URL

- Default development API Base URL (from environment.ts): `https://localhost:5001/api/`

---

# Summary of Required Endpoints

- Authentication
  - GET /api/Authentication
  - POST /api/Authentication/login
  - POST /api/Authentication/register
  - GET /api/Authentication/emailexists?email={email}
  - GET /api/Authentication/address
  - PUT /api/Authentication/address

- Products / Shop
  - GET /api/products
    - query: brandId, typeId, search, sort, pageIndex, pageSize
  - GET /api/products/{id}
  - GET /api/products/brands
  - GET /api/products/types

- Basket / Payments
  - GET /api/baskets?id={basketId}
  - POST /api/baskets
  - DELETE /api/baskets?id={basketId}
  - POST /api/payments/{basketId}

- Orders / Checkout
  - POST /api/orders
  - GET /api/orders
  - GET /api/orders/{id}
  - GET /api/orders/deliveryMethods

# Endpoint Definitions

## Authentication

### GET /api/Authentication

### Request DTO

```
(none)
```

### Response DTO

```
interface IUser {
  email: string;
  displayName: string;
  userName: string;
  token: string; // JWT
}
```

### Notes

- Requires `Authorization: Bearer {token}` header.
- Returns current user info and token (frontend stores `token` in localStorage key `token`).
- Status codes: 200 OK with IUser; 401 Unauthorized when token invalid.

---

### POST /api/Authentication/login

### Request DTO

```
interface LoginRequest {
  email: string;
  password: string;
}
```

### Response DTO

```
interface IUser {
  email: string;
  displayName: string;
  userName: string;
  token: string;
}
```

### Notes

- Expected 200 OK with IUser on success.
- On validation or credentials failure: 400 or 401 with error shape `error: { message, statusCode }` or `error.errors` (validation array).
- Frontend saves `token` to localStorage and uses it on subsequent requests.

---

### POST /api/Authentication/register

### Request DTO

```
interface RegisterRequest {
  displayName: string;
  email: string;
  password: string;
}
```

### Response DTO

```
interface IUser { // same as login response
  email: string;
  displayName: string;
  userName: string;
  token: string;
}
```

### Notes

- Returns validation errors in `error.errors` array on 400; frontend assigns `this.errors = error.errors`.
- On success, saves token and navigates to /shop.

---

### GET /api/Authentication/emailexists?email={email}

### Request DTO

```
(none, query param)
```

### Response DTO

```
boolean
```

### Notes

- Returns `true` if email exists, `false` otherwise.
- Used by async validator during registration.
- No Authorization header required.

---

### GET /api/Authentication/address

### Request DTO

```
(none)
```

### Response DTO

```
interface IAddress {
  firstName: string;
  lastName: string;
  street: string;
  city: string;
  country: string;
}
```

### Notes

- Requires `Authorization: Bearer {token}` header.
- Returns user's saved address.

---

### PUT /api/Authentication/address

### Request DTO

```
interface IAddress {
  firstName: string;
  lastName: string;
  street: string;
  city: string;
  country: string;
}
```

### Response DTO

```
interface IAddress (updated)
```

### Notes

- Requires Authorization header.
- Expected status codes: 200 OK with updated address, 400 for validation errors.

---

## Products / Shop

### GET /api/products

### Request DTO (query params)

```
brandId: number (optional)
typeId: number (optional)
search: string (optional)
sort: string (required) // frontend uses nameAsc default, other values: name, priceAsc, priceDesc
pageIndex: number (pageNumber in frontend ShopParams)
pageSize: number
```

### Response DTO

```
interface IPagination {
  pageIndex: number;
  pageSize: number;
  count: number; // total count
  data: IProduct[];
}

interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  pictureUrl: string;
  productType: string;
  productBrand: string;
}
```

### Notes

- Frontend expects the response in a full HTTP response object (observe: 'response') and reads `response.body.data` and sets pagination to `response.body`.
- Frontend assigns `this.totalCount = response['totalCount']` in the Shop component (note: slight mismatch: pagination property named `count` in model and `totalCount` access by UI). Both should be provided. Best to include both `count` and `totalCount` (or alias in server) for compatibility.
- Expected status 200 OK.

---

### GET /api/products/{id}

### Request DTO

```
Route parameter: id (number)
```

### Response DTO

```
IProduct
```

### Notes

- Returns a single product.

---

### GET /api/products/brands

### Response DTO

```
IBrand[]
interface IBrand { id: number; name: string }
```

### Notes

- Returns list of brands.

---

### GET /api/products/types

### Response DTO

```
IType[]
interface IType { id: number; name: string }
```

---

## Basket / Payments

### GET /api/baskets?id={basketId}

### Request DTO

```
Query param: id (string)
```

### Response DTO

```
interface IBasket {
  id: string;
  items: IBasketItem[];
  clientSecret?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
  shippingPrice?: number;
}

interface IBasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}
```

### Notes

- Used by frontend to initialize basket from localStorage `basket_id`.
- Returns shippingPrice and other payment fields when relevant.

---

### POST /api/baskets

### Request DTO

```
IBasket (full basket object)
```

### Response DTO

```
IBasket (server saved)
```

### Notes

- The frontend calls this to create or update the basket. On success, response is used to update local basket state.
- Expected Content-Type: application/json

---

### DELETE /api/baskets?id={basketId}

### Request DTO

```
Query param: id (string)
```

### Response DTO

```
204 No Content (or 200)
```

### Notes

- Frontend handles success by clearing local basket and removing localStorage key `basket_id`.

---

### POST /api/payments/{basketId}

### Request DTO

```
(none body, or empty object {})
```

### Response DTO

```
IBasket (updated with clientSecret and paymentIntentId)
```

### Notes

- Called by `createPaymentIntent()` in BasketService. Server is expected to create/return Stripe PaymentIntent client secret and update basket with `clientSecret` and `paymentIntentId`.
- Payment flow: frontend uses Stripe JS (public key embedded) and calls `stripe.confirmCardPayment(basket.clientSecret, { payment_method: {...} })`.
- Server must create a PaymentIntent and return `clientSecret` string in the basket's `clientSecret` property.
- Also expect webhook endpoint on backend to update order/payment status (frontend comments refer to webhook invocation). No frontend call to webhook; server must implement.

---

## Orders / Checkout

### POST /api/orders

### Request DTO

```
interface IOrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shipToAddress: IAddress;
}
```

### Response DTO

```
interface IOrder {
  id: string;
  buyerEmail: string;
  orderDate: string; // ISO string
  shipToAddress: IAddress;
  deliveryMethod: string;
  deliveryCost: number;
  items: IOrderItem[];
  subtotal: number;
  status: string;
  total: number;
}

interface IOrderItem {
  productId: string;
  productName: string;
  pictureUrl: string;
  price: number;
  quantity: number;
}
```

### Notes

- Authorization: Likely requires authenticated user (JwtInterceptor adds Authorization header for all requests when token present; checkout.createOrder called from checkout flow under authenticated session).
- On success, frontend expects created order returned, and uses navigation extras to show order on success page.

---

### GET /api/orders

### Request DTO

```
(none)
```

### Response DTO

```
IOrder[]
```

### Notes

- Requires Authorization header. Used by OrdersService.getOrdersForUser().

---

### GET /api/orders/{id}

### Request DTO

```
Route param: id (string)
```

### Response DTO

```
IOrder
```

### Notes

- Requires Authorization header.

---

### GET /api/orders/deliveryMethods

### Request DTO

```
(none)
```

### Response DTO

```
IDeliveryMethod[]
interface IDeliveryMethod {
  shortName: string;
  deliveryTime: string;
  description: string;
  cost: number;
  id: number;
}
```

### Notes

- Used during checkout to present delivery options.

---

# DTO Naming Mismatches

Frontend expects (exact names):

```
clientSecret
paymentIntentId
shippingPrice
productType
productBrand
pageIndex
pageSize
count
data
basketId
deliveryMethodId
shipToAddress
buyerEmail
orderDate
deliveryMethod
deliveryCost
subtotal
status
total
pictureUrl
productName
productId
firstName
lastName
```

Backend may commonly use PascalCase or different names, example mismatches to check:

- `clientSecret` vs `ClientSecret`
- `paymentIntentId` vs `PaymentIntentId`
- `shippingPrice` vs `ShippingPrice`
- `productType` / `productBrand` vs `productTypeId` / `brandName` (ensure string names used)
- Pagination: frontend reads `count` but UI reads `totalCount` — ensure server provides either both or mapping.
- `pageIndex` vs `pageNumber` (frontend uses `pageNumber` in ShopParams but expects `pageIndex` in pagination response)

Developers should maintain camelCase JSON property naming (default in ASP.NET Core: PascalCase). Use JSON serializer options to set PropertyNamingPolicy = CamelCase.

---

# Enum Mappings

Frontend uses `sort` param values from `ShopParams`:

- `nameAsc` (default)
- `name` (used in UI list)
- `priceAsc`
- `priceDesc`

Backend should accept these string values and implement sorting accordingly.

Other enums: `status` in `IOrder` is a string. Backend should map order statuses to strings the frontend expects (e.g., "Pending", "PaymentReceived", "Shipped", etc.). No explicit enum file in frontend; keep string-based statuses.

---

# Authentication Contract

Login request

```
POST /api/Authentication/login
body: { email, password }
response: IUser { email, displayName, userName, token }
```

Register request

```
POST /api/Authentication/register
body: { displayName, email, password }
response: IUser
```

Refresh token request

- Frontend does not implement refresh token flow. Only a JWT `token` is stored in localStorage. If refresh tokens are added, ensure compatibility.

JWT payload expectations

- Token is a standard JWT string placed in `IUser.token`.
- JwtInterceptor sets header: `Authorization: Bearer {token}` for all requests if present.

Required claims

- Token must be verifiable; backend must accept token and return 401 when invalid.
- Frontend does not inspect token claims; however server must include user identification (email or username) to support endpoints like GET /api/Authentication returning `IUser`.

---

# Pagination Contract

Frontend expects pagination response in this structure (camelCase):

```
{
  "pageIndex": number,
  "pageSize": number,
  "count": number,
  "data": [ ... ]
}
```

Notes: Frontend also references `totalCount` in several components. Backend should provide `count` and/or `totalCount` (or configure frontend to map `count` to `totalCount`). Ensure property names are camelCase.

---

# Environment Configuration

- API Base URL: `https://localhost:5001/api/` (set in `src/environments/environment.ts`).
- Stripe public key: hard-coded in `checkout-payment.component.ts` (development pk_test key). Backend must use corresponding Stripe secret key and implement PaymentIntent creation on `POST /api/payments/{basketId}` and webhooks.
- SignalR: No hubs detected in frontend.
- External services: Stripe (payments). No other external services referenced.

---

# Backend Compatibility Report (Checklist)

- Authentication
  - [ ] GET /api/Authentication -> returns IUser when Authorization header provided
  - [ ] POST /api/Authentication/login
  - [ ] POST /api/Authentication/register
  - [ ] GET /api/Authentication/emailexists?email=
  - [ ] GET /api/Authentication/address (auth required)
  - [ ] PUT /api/Authentication/address (auth required)

- Products
  - [ ] GET /api/products (with query params brandId,typeId,search,sort,pageIndex,pageSize) -> returns IPagination (camelCase)
  - [ ] GET /api/products/{id}
  - [ ] GET /api/products/brands
  - [ ] GET /api/products/types

- Basket/Payments
  - [ ] GET /api/baskets?id=
  - [ ] POST /api/baskets
  - [ ] DELETE /api/baskets?id=
  - [ ] POST /api/payments/{basketId} -> create Stripe PaymentIntent and return clientSecret and paymentIntentId in IBasket
  - [ ] Implement Stripe webhook to update payments/orders

- Orders
  - [ ] POST /api/orders
  - [ ] GET /api/orders (auth)
  - [ ] GET /api/orders/{id} (auth)
  - [ ] GET /api/orders/deliveryMethods

- DTO mismatches
  - [ ] Ensure JSON property casing is camelCase (configure ASP.NET Core). Example: `clientSecret`, `paymentIntentId`, `shippingPrice`, `productType`, `productBrand`, `pageIndex`, `pageSize`, `count`, `data`.

- Authentication mismatches
  - [ ] Token format: frontend expects JWT string in `user.token` and uses it in `Authorization` header.

- Serialization mismatches
  - [ ] Ensure dates use ISO strings for `orderDate`.
  - [ ] Ensure numbers and booleans are typed properly.

- Date formatting requirements
  - [ ] Use ISO-8601 date strings for `orderDate`.

---

# Error Response Contract

Frontend's `ErrorInterceptor` expects error responses as either:

- Validation errors: error.status === 400 and `error.error.errors` present -> thrown and read by components.
- Single error: `error.error.message` and `error.error.statusCode` used for toastr messages.
- For 401: show toast with `error.error.message`.
- For 404: redirect to `/not-found`.
- For 500: route to `/server-error` with `state: { error: error.error }`.

Therefore server should produce structured error JSON such as:

```
{ "message": "Human readable message", "statusCode": 400 }

or

{ "errors": ["e1","e2"] }
```

---

# File Upload / Download

- No file upload or direct download endpoints were found in the frontend.

---

# Payment Flow Summary

1. Frontend stores basket with POST /api/baskets and receives basket with `clientSecret` and `paymentIntentId` when backend creates a PaymentIntent.
2. Frontend calls POST /api/payments/{basketId} to (re)create PaymentIntent; backend returns basket with `clientSecret`.
3. Frontend calls Stripe.js `stripe.confirmCardPayment(basket.clientSecret, {...})` to confirm payment.
4. Backend must implement Stripe webhook to update order/payment status.

---

# Basket / Cart Endpoints (Summary)

- GET /api/baskets?id={id}
- POST /api/baskets (body: IBasket)
- DELETE /api/baskets?id={id}
- POST /api/payments/{basketId} (returns IBasket with payment data)

---

# Order Endpoints (Summary)

- POST /api/orders (body: IOrderToCreate) -> returns IOrder
- GET /api/orders -> IOrder[]
- GET /api/orders/{id} -> IOrder
- GET /api/orders/deliveryMethods -> IDeliveryMethod[]

---

# Additional Implementation Notes for ASP.NET Core

- Use camelCase JSON serialization: `options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase`.
- Configure JWT bearer authentication and ensure `Authorization: Bearer {token}` is validated.
- Implement model validation and return `errors` array for 400 responses when model state invalid.
- Implement Stripe PaymentIntent creation on `POST /api/payments/{basketId}` and expose `clientSecret` in the returned basket.
- Provide `count` in pagination responses and optionally `totalCount` for UI reads.
- Order `orderDate` should be an ISO-8601 string.

---

# Contact

If something in the frontend is ambiguous, inspect the following frontend files:

- [src/app/account/account.service.ts](src/app/account/account.service.ts#L1)
- [src/app/shop/shop.service.ts](src/app/shop/shop.service.ts#L1)
- [src/app/basket/basket.service.ts](src/app/basket/basket.service.ts#L1)
- [src/app/checkout/checkout.service.ts](src/app/checkout/checkout.service.ts#L1)
- [src/app/orders/orders.service.ts](src/app/orders/orders.service.ts#L1)

---

Generated by GitHub Copilot (GPT-5 mini) on 2026-06-06.
