# TopUpAPI


## Installation and Setup:

1- clone the repo
```bash
git clone https://github.com/Ali-y-Suliman/TopUpAPI.git
```

2- install dotnet ef:
```bash
dotnet tool install --global dotnet-ef
```

3- run dotnet build:
```bash
dotnet build
```

4- create the database:
```bash
dotnet ef database update
```

5- run the project:
```bash
dotnet run
```

**Note**: to run swagger:
```bash
dotnet watch run
```

---

## Usage:

TopUpAPI provides the following endpoints:

* POST  /api/Users - Add a new user in the system.

* POST  /api/TopUpOption - Add a new TopUpOption in the system.

* GET  /api/TopUpOption - Retrieves all the TopUpOptions.

* POST  /api/TopUpBeneficiary - Add a new TopUpBeneficiary in the system.

* GET  /api/TopUpOption - Retrieves all the TopUpBeneficiaries.

* POST  /api/UsersTopUpBeneficiaries - Add a new UsersTopUpBeneficiaries in the system.

* GET  /api/UsersTopUpBeneficiaries/{userId} - Retrieves all the UsersTopUpBeneficiaries for a specific user.

* POST  /api/UsersTopUpBeneficiaries/{id} - Update the activation status for a specific UsersTopUpBeneficiary in the system.
