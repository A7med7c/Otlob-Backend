# Improvement Roadmap

## Objective
Stabilize and productionize Otlob Backend by addressing security, quality, architecture, and operations gaps.

## Priority Backlog

| Priority | Severity | Area | Location | Improvement | Recommended Action |
|---|---|---|---|---|---|
| P0 | Critical | Security | `/tmp/workspace/A7med7c/Otlob-Backend/E-Commerce.Web/appsettings.json:20` | Hardcoded JWT secret | Remove from source, rotate key, load from secret manager |
| P0 | High | Security | `/tmp/workspace/A7med7c/Otlob-Backend/E-Commerce.Web/appsettings.json:10-11` | Hardcoded DB connection data | Externalize connection strings per environment |
| P0 | High | Security | `/tmp/workspace/A7med7c/Otlob-Backend/Infrastructure/Persistence/DataSeeder.cs:123-124` | Weak fixed seed passwords | Replace with secure bootstrap flow + forced reset |
| P0 | High | Reliability | `/tmp/workspace/A7med7c/Otlob-Backend/Infrastructure/PresentationLayer/Attributes/CashAttribute.cs:17` + DI | Missing cache DI registration | Register cache services and add startup validation |
| P1 | Medium | Authorization | `/tmp/workspace/A7med7c/Otlob-Backend/Infrastructure/PresentationLayer/Controllers/OrderController.cs:26` | Order retrieval lacks authorization ownership guard | Add `[Authorize]` + scope order to current user |
| P1 | Medium | Performance | `/tmp/workspace/A7med7c/Otlob-Backend/Core/ServiceLayer/OrderService.cs:27-31` | Potential N+1 query when creating orders | Batch load products by IDs |
| P1 | Medium | API Design | `/tmp/workspace/A7med7c/Otlob-Backend/Shared/ProductQueryParams.cs` | Paging defaults unsafe | Enforce default/min constraints |
| P1 | Medium | Quality | `/tmp/workspace/A7med7c/Otlob-Backend/Core/ServiceLayer/ServicesManager.cs` | Service locator-like coupling | Refactor to direct DI service interfaces |
| P2 | Medium | Ops/DevOps | repository root | No CI/CD workflows | Add GitHub Actions for build/test/security scan |
| P2 | Medium | Ops/DevOps | repository root | No Docker support | Add Dockerfile + compose for API/SQL/Redis local stack |
| P2 | Medium | QA | solution-wide | No test projects | Add unit/integration test projects and quality gates |
| P3 | Low | Maintainability | multiple (`Sevice`, `Cash`) | Naming inconsistencies | Rename namespaces/files for consistency |
| P3 | Low | Observability | host startup | Limited logging/monitoring | Add structured logs, health checks, metrics, tracing |

## Suggested Delivery Phases

### Phase A: Security & Runtime Stability (Immediate)
- Externalize secrets and rotate compromised values
- Fix cache DI registration
- Lock down order-by-id endpoint authorization
- Remove silent exception swallowing in seeding

### Phase B: Quality Baseline
- Introduce tests (services, controllers, integration)
- Enforce static analysis and warnings as quality gate
- Strengthen request validation

### Phase C: Architecture Hardening
- Replace `ServicesManager` with direct DI
- Consolidate duplicated specification logic
- Standardize naming and endpoint conventions

### Phase D: DevOps & Production Readiness
- Add CI pipelines (build, test, security scans)
- Add containerization
- Add environment-specific deployment strategy
- Add monitoring and alerting

## Success Metrics
- 0 critical/high security findings
- Successful automated build/test on each PR
- Minimum baseline test coverage established
- Mean time to detect runtime failures reduced via monitoring
