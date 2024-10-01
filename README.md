## Quick Start
Follow these steps:
- Install PostgreSQL (port: 5432).
- Add user `postgres` with password `postgres` if it wasn't added automatically.
- Make sure user `postgres` has password `postgres`.
- Make sure there is no table named `delivery` in the database.
- Make sure port `5131` is available on your computer.
- Clone repository.
    ```
    git clone https://github.com/yakovypg/Delivery-MVC.git
    ```
- Open created folder.
	```
	cd Delivery-MVC
	```
- Run application.
	```
	dotnet run --project Delivery
	```
- Open browser and go to the `http://localhost:5131`.
