# 🚗 Car Insurance Company System

This is a complete database-driven desktop application simulating the operations of a car insurance company.  
It includes database design, implementation, GUI interface using Windows Forms, and full documentation.

## 📁 Project Structure

- **Database/**
  - `Physical model.png` → Physical model
  - `Conceptual model.png` → Conceptual model
  - `Car Insurance SQL.sql` → SQL script to create tables and insert sample data

- **CarInsuranceAppCode/**
  - `CarInsuranceApp.sln` → Visual Studio Solution File
  - `CarInsuranceApp/` → Folder containing all Windows Forms code files (.cs, .Designer.cs)

- **Documentation/**
  - `Report.pdf` → Full explanation of the system (in Arabic)


## 🛠️ Technologies Used

- MS SQL Server for the database
- C# (Windows Forms) for the user interface
- Visual Studio for development

## 📌 Features

- The system shall allow users to sign in 
either as customers or administrators. 
Authentication shall be performed using 
the user's name, email, phone number, 
address, and password.
- The system shall allow the creation of a 
new customer account. A unique 
customer ID shall be generated, and the 
following details shall be stored: name, 
email, phone number, address, and 
password. Customers can be added by 
themselves or by an administrator.
- The system shall allow existing customers 
to update their registered information, 
including their name, contact details, and 
address.
- The system shall allow customers to 
delete their own accounts. Additionally, 
administrators shall have the ability to 
remove customer accounts from the 
system.
- The system shall allow customers to 
register a new car either by themselves or 
through the administrator. Each car shall 
be assigned a unique identifier and store 
information such as license plate number, 
model, and manufacturing year.
-    The system shall allow customers or 
administrators to modify existing car 
details, including the license plate 
number, model, and year of manufacture.   
The system must ensure that all 
ownership relationships and related 
records are consistently updated to 
reflect any changes.
- The system shall allow either a customer 
or an admin to delete a saved car record. 
Upon deletion, the system must 
automatically update or remove all 
associated relationships and references— 
such as customer ownership links or 
dependent recordsto maintain data 
integrity and ensure consistency across 
the system.
- In the event of an accident, the system 
shall allow either the customer or the 
admin to add detailed information about 
the accident. Each accident record must 
include a unique accident ID, the date of 
the accident, the location, and a 
description of the event. The system must 
also support many-to-many relationships 
between accidents and cars; a single 
accident may involve multiple cars, and a 
single car may be associated with multiple 
accident records.
- The system must let customers or admins 
edit details of an existing accident, like 
the date, location, or description. Any 
changes should keep all links between accidents and cars consistent, so no data 
gets messed up. The system needs to 
update all related records properly to 
reflect the new info.
- The system should allow customers and 
admins to check out full details of an 
accident, including its ID, date, location, 
description, and a list of all cars involved. 
It should also let users filter accidents by 
stuff like date, car, or customer to quickly 
find what they need.
- The system needs to create monthly 
reports that sum up all accidents for a 
specific month. The report should show 
the total number of accidents, how many 
cars were involved, and a breakdown by 
car model. Admins should be able to 
download it as a PDF for easy sharing or 
analysis.

## ▶️ How to Run

1. Open `CarInsuranceSystem.sln` in Visual Studio.
2. Make sure SQL Server is running and execute `CarInsurance.sql` to create the database.
3. Update the database connection string in the code to match your setup.
4. Build and run the project.

## 🧾 Documentation

A detailed project explanation is available in:
/Documentation/Report.pdf


تحرير

## 👨‍💻 Author

Developed by: **Mahmoud Ahmed**  
GitHub: [MahmoudAhmed150](https://github.com/MahmoudAhmed150)

## 📄 License

This project is for educational and demo purposes only.
