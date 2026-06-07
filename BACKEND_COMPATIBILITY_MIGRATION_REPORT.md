# Backend Compatibility Migration Report

Source contract: `FRONTEND_API_CONTRACT.md`

## Audit Findings

- Authentication route mismatch: backend exposed `/api/User`; Angular expects `/api/Authentication`.
- Email availability route mismatch: backend exposed `CheckEmail`; Angular expects `emailexists?email=`.
- User response mismatch: `userName` was missing from `IUser`.
- Register request mismatch: backend required/used `userName` and `phone`; Angular sends only `displayName`, `email`, and `password`.
- Product detail auth mismatch: `GET /api/products/{id}` required auth; Angular contract expects public access.
- Product query mismatch: backend used `SearchPhrase` and `sortingOptions`; Angular sends `search` and `sort`.
- Product DTO mismatch: backend returned `imageUrl`, `brandName`, and `typeName`; Angular expects `pictureUrl`, `productBrand`, and `productType`.
- Pagination mismatch: backend returned only `totalCount`; Angular contract expects `count` and also references `totalCount`.
- Basket route mismatch: backend used path ids; Angular calls query routes using `?id=`.
- Basket DTO mismatch: payment fields and basket item `brand/type` were missing.
- Payment endpoint missing: `POST /api/payments/{basketId}` was not exposed.
- Payment webhook missing: no endpoint updated order payment status from Stripe webhooks.
- Order request mismatch: backend expected `address`; Angular sends `shipToAddress`.
- Order response mismatch: backend returned `userEmail`, `shippingAddress`, `deliveyMethod`, `orderStatus`, and `subTotal`; Angular expects `buyerEmail`, `shipToAddress`, `deliveryMethod`, `status`, and `subtotal`.
- Delivery method response mismatch: backend returned `price`; Angular expects `cost`.
- Validation error shape mismatch: backend returned `validationErrors`; Angular reads `error.errors`.
- JSON/auth issue: JWT challenges could return empty 401 responses, while Angular expects a structured error body.

## Modified Files

- `Shared/ProductQueryParams.cs`: Added Angular query names `search` and `sort`, default paging, and accepted sort values `nameAsc`, `name`, `nameDesc`, `priceAsc`, and `priceDesc`.
- `Shared/PaginatedResult.cs`: Added `count` while keeping `totalCount` for frontend compatibility.
- `Shared/DTOs/Product/ProductDto.cs`: Renamed API response properties to `pictureUrl`, `productBrand`, and `productType`.
- `Shared/DTOs/Basket/CustomerBasketDto.cs`: Added `clientSecret`, `paymentIntentId`, `deliveryMethodId`, and `shippingPrice`.
- `Shared/DTOs/Basket/BasketItemsDto.cs`: Added `brand` and `type`.
- `Core/DomainLayer/Models/BasketItems.cs`: Added `Brand` and `Type` so Redis basket persistence preserves Angular basket item fields.
- `Shared/DTOs/Identity/UserDto.cs`: Added `userName`.
- `Shared/DTOs/Identity/RegisterDto.cs`: Made `userName` and `phone` optional to accept Angular registration payloads.
- `Shared/DTOs/Order/OrderDto.cs`: Added `shipToAddress` while retaining `address` as a compatibility fallback.
- `Shared/DTOs/Order/ReturnedOrderDto.cs`: Changed response shape to Angular order contract, including `buyerEmail`, `shipToAddress`, `deliveryMethod`, `deliveryCost`, `status`, and JSON name `subtotal`.
- `Shared/DTOs/Order/OrderItemDto.cs`: Added `productId`.
- `Shared/DTOs/Order/DeliveryMethodDto.cs`: Changed delivery price property to `cost`.
- `Shared/CutomResponses/ValidationErrorResponse.cs`: Added flat `errors` array while keeping structured validation details.
- `Core/ServiceLayer/ProductService.cs`: Returned requested `pageSize` instead of current item count in pagination metadata.
- `Core/ServiceLayer/Specifications/ProductWithTypesandBrandsSpecifications.cs`: Switched filtering/sorting to Angular query fields.
- `Core/ServiceLayer/Specifications/TotalRecordsSpecifications.cs`: Switched search filtering to Angular query field.
- `Core/ServiceLayer/MappingProfiles/ProductProfile.cs`: Mapped product entities into Angular product DTO property names.
- `Core/ServiceLayer/MappingProfiles/ImageResolver.cs`: No functional code change; file is marked modified only by line-ending normalization during the DTO mapping update pass.
- `Core/ServiceLayer/MappingProfiles/OrderProfile.cs`: Mapped order entities into Angular order DTO property names and totals.
- `Core/ServiceLayer/OrderService.cs`: Accepted `shipToAddress` and retained `address` fallback.
- `Core/ServiceLayer/AuthenticationService.cs`: Returned `userName` and derived username from email when the frontend omits it.
- `Core/SeviceImplementation/IPaymentService.cs`: Added payment status update operation for webhook handling.
- `Core/ServiceLayer/PaymentService.cs`: Added order payment status update by PaymentIntent id.
- `Core/ServiceLayer/ServicesManager.cs`: Added `PaymentService` implementation to satisfy the service manager contract.
- `Infrastructure/PresentationLayer/Controllers/ApiBaseController.cs`: Removed inherited route so derived controllers can expose exact contract routes.
- `Infrastructure/PresentationLayer/Controllers/UserController.cs`: Exposed auth endpoints under `/api/Authentication` and `emailexists`.
- `Infrastructure/PresentationLayer/Controllers/ProductsController.cs`: Exposed `/api/products`, removed auth from product detail, and fixed pagination action type.
- `Infrastructure/PresentationLayer/Controllers/BasketsController.cs`: Added query-based `GET` and `DELETE` routes while preserving existing path routes.
- `Infrastructure/PresentationLayer/Controllers/OrderController.cs`: Exposed `/api/orders` and lowercase `deliveryMethods`.
- `Infrastructure/PresentationLayer/Controllers/PaymentsController.cs`: Added `POST /api/payments/{basketId}` and `POST /api/payments/webhook`.
- `E-Commerce.Web/Program.cs`: Enforced camelCase JSON, enum string serialization, and fixed the missing CORS semicolon.
- `E-Commerce.Web/Extentions/ServicesExtensions.cs`: Added structured JSON 401 responses for JWT challenges.
- `E-Commerce.Web/Factories/ApiResponseFactory.cs`: Flattened validation errors into `errors` for Angular while retaining detailed validation metadata.
- `E-Commerce.Web/CustomMiddleWares/CustomExceptionHandlerMiddleware.cs`: Ensured exception responses are camelCase `{message,statusCode,errors}`.

## Compatibility Result

- Required Angular endpoints are now exposed.
- Request and response DTOs now match the frontend property names.
- Pagination now includes both `count` and `totalCount`.
- Product sorting accepts the frontend enum/string values.
- Authentication flow returns `IUser` with `token` and `userName`.
- Error responses now support Angular's `error.errors` and `error.message/statusCode` reads.

## Verification

- `dotnet build .\E-Commerce.slnx` completed successfully with 0 warnings and 0 errors.
