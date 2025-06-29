CREATE DATABASE CarInsuranceDB
USE CarInsuranceDB;

/*==============================================================*/
/* Table: ACCIDENT                                              */
/*==============================================================*/
create table ACCIDENT
(
   ACCIDENTID           int primary key identity(1,1),
   DATE                 date not null,
   LOCATION             varchar(100),
   DESCRIPTION          varchar(200)
);

/*==============================================================*/
/* Table: CAR                                                   */
/*==============================================================*/
create table CAR
(
   CARID                int primary key identity(1,1),
   LINCESPLATE          nvarchar(10) not null unique,
   MODEL                varchar(50) not null,
   YEAR                 int not null CHECK (Year <= 2025)
);

/*==============================================================*/
/* Table: CLAIM                                                 */
/*==============================================================*/
create table CLAIM
(
   ACCIDENTID           int,
   CLAIMID              int identity(1,1),
   AMOUNT               numeric(10,2) not null CHECK (Amount >= 0),
   STATUS               varchar(20) not null CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
   primary key (ACCIDENTID, CLAIMID),
   foreign key (ACCIDENTID) references ACCIDENT (ACCIDENTID) ON DELETE CASCADE
);

/*==============================================================*/
/* Table: CUSTOMER                                              */
/*==============================================================*/
create table CUSTOMER
(
   CUSTOMERID           int primary key identity(1,1),
   NAME                 varchar(100) not null,
   EMAIL                varchar(100) not null unique,
   PHONE                varchar(11),
   ADDRESS              varchar(200),
   PASSWORD             nvarchar(max) not null
);

/*==============================================================*/
/* Table: INSURANCEPOLICY                                       */
/*==============================================================*/
create table INSURANCEPOLICY
(
   POLICYID             int primary key identity(1,1),
   CARID                int not null,
   STARTDATE            date not null,
   ENDDATE              date not null,
   COVERAGETYPE         varchar(50) not null,
   foreign key (CARID) references CAR (CARID) ON DELETE CASCADE ,
   CHECK (EndDate > StartDate)
);

/*==============================================================*/
/* Table: INVOLVEDIN                                            */
/*==============================================================*/
create table INVOLVEDIN
(
   CARID                int,
   ACCIDENTID           int,
   primary key (CARID, ACCIDENTID),
   foreign key (CARID) references CAR (CARID) ON DELETE CASCADE ,
   foreign key (ACCIDENTID) references ACCIDENT (ACCIDENTID) ON DELETE CASCADE
);

/*==============================================================*/
/* Table: OWNS                                                  */
/*==============================================================*/
create table OWNS
(
   CUSTOMERID           int,
   CARID                int,
   primary key (CUSTOMERID, CARID),
   foreign key (CUSTOMERID) references CUSTOMER (CUSTOMERID) ON DELETE CASCADE ,
   foreign key (CARID) references CAR (CARID) ON DELETE CASCADE
);

/*==============================================================*/
/* Table: REPAIRSHOP                                            */
/*==============================================================*/
create table REPAIRSHOP
(
   SHOPID               int primary key identity(1,1),
   SHOPNAME             varchar(100) not null,
   LOCATION             varchar(100),
   CONTACTNUMBER        varchar(11)
);

/*==============================================================*/
/* Table: REPAIRS                                               */
/*==============================================================*/
create table REPAIRS
(
   ACCIDENTID           int,
   SHOPID               int,
   primary key (ACCIDENTID, SHOPID),
   foreign key (ACCIDENTID) references ACCIDENT (ACCIDENTID) ON DELETE CASCADE,
   foreign key (SHOPID) references REPAIRSHOP (SHOPID) ON DELETE CASCADE
);

-- Insert into Customer
INSERT INTO CUSTOMER (Name, Email, Phone, Address, Password)
VALUES 
('Ahmed Mohamed', 'ahmed1@example.com', '0123456789', '123 Cairo St', 'pass123'),
('Sara Ali', 'sara1@example.com', '0119876543', '456 Giza St', 'pass456'),
('Mohamed Hassan', 'mohamed2@example.com', '0101234567', '789 Alex St', 'pass789'),
('Fatima Omar', 'fatima1@example.com', '0159876543', '321 Nasr St', 'pass321'),
('Khaled Ibrahim', 'khaled1@example.com', '0128765432', '654 Maadi St', 'pass654'),
('Nourhan Adel', 'nourhan1@example.com', '0112345678', '987 Heliopolis St', 'pass987'),
('Youssef Amr', 'youssef1@example.com', '0109876543', '147 Zamalek St', 'pass147'),
('Amina Mostafa', 'amina1@example.com', '0151234567', '258 Dokki St', 'pass258'),
('Omar Khaled', 'omar1@example.com', '0127654321', '369 Mohandessin St', 'pass369'),
('Laila Samir', 'laila1@example.com', '0118765432', '741 Shubra St', 'pass741'),
('Hassan Tarek', 'hassan1@example.com', '0103456789', '852 Imbaba St', 'pass852'),
('Mona Zaki', 'mona1@example.com', '0156789012', '963 Haram St', 'pass963'),
('Tamer Hosny', 'tamer1@example.com', '0125432109', '159 Nasr City St', 'pass159'),
('Dina Emad', 'dina1@example.com', '0114321098', '357 6th Oct St', 'pass357'),
('Ali Nour', 'ali1@example.com', '0109871234', '468 Sheikh Zayed St', 'pass468'),
('Rania Youssef', 'rania1@example.com', '0153210987', '579 New Cairo St', 'pass579'),
('Karim Adel', 'karim1@example.com', '0121098765', '680 Tagamoa St', 'pass680'),
('Salma Hany', 'salma1@example.com', '0118761234', '791 Madinaty St', 'pass791'),
('Adel Mahmoud', 'adel1@example.com', '0105432109', '802 Rehab St', 'pass802'),
('Hoda Gamal', 'hoda1@example.com', '0159873210', '913 Obour St', 'pass913'),
('Amr Diab', 'amr1@example.com', '0123459876', '135 Sahel St', 'pass135'),
('Mai Ezz', 'mai1@example.com', '0116789012', '246 Marina St', 'pass246'),
('Sami Yassin', 'sami1@example.com', '0101239876', '357 Hurghada St', 'pass357'),
('Lina Tarek', 'lina1@example.com', '0154567890', '468 Sharm St', 'pass468'),
('Fady Nabil', 'fady1@example.com', '0127890123', '579 Luxor St', 'pass579'),
('Reem Sami', 'reem1@example.com', '0112340987', '680 Aswan St', 'pass680'),
('Nader Hamed', 'nader1@example.com', '0105678901', '791 Suez St', 'pass791'),
('Aya Khaled', 'aya1@example.com', '0158901234', '802 Ismailia St', 'pass802'),
('Tarek Nour', 'tarek1@example.com', '0121234567', '913 Port Said St', 'pass913'),
('Maha Sami', 'maha1@example.com', '0114567890', '135 Damietta St', 'pass135'),
('Ehab Tawfik', 'ehab1@example.com', '0107890123', '246 Mansoura St', 'pass246'),
('Noha Adel', 'noha1@example.com', '0150123456', '357 Tanta St', 'pass357'),
('Sherif Omar', 'sherif1@example.com', '0122345678', '468 Zagazig St', 'pass468'),
('Marwa Hany', 'marwa1@example.com', '0115678901', '579 Banha St', 'pass579'),
('Hany Ramzy', 'hany1@example.com', '0108901234', '680 Shibin St', 'pass680'),
('Dalia Mostafa', 'dalia1@example.com', '0151234567', '791 Menouf St', 'pass791'),
('Kareem Sami', 'kareem1@example.com', '0123456789', '802 Kafr El Sheikh St', 'pass802'),
('Rasha Nour', 'rasha1@example.com', '0116789012', '913 Damanhour St', 'pass913'),
('Wael Kfoury', 'wael1@example.com', '0109012345', '135 Rosetta St', 'pass135'),
('Mona Adel', 'mona2@example.com', '0152345678', '246 Edfu St', 'pass246'),
('Tamer Ashour', 'tamer2@example.com', '0125678901', '357 Kom Ombo St', 'pass357'),
('Soha Khaled', 'soha1@example.com', '0117890123', '468 Esna St', 'pass468'),
('Hatem Fahmy', 'hatem1@example.com', '0100123456', '579 Qena St', 'pass579'),
('Rana Sami', 'rana1@example.com', '0153456789', '680 Sohag St', 'pass680'),
('Nabil Ali', 'nabil1@example.com', '0126789012', '791 Assiut St', 'pass791'),
('Heba Nour', 'heba1@example.com', '0119012345', '802 Minya St', 'pass802'),
('Adham Sami', 'adham1@example.com', '0102345678', '913 Beni Suef St', 'pass913'),
('Lara Khaled', 'lara1@example.com', '0154567890', '135 Fayoum St', 'pass135'),
('Samer Hany', 'samer1@example.com', '0127890123', '246 Giza St', 'pass246'),
('Nour Sami', 'nour1@example.com', '0110123456', '357 Cairo St', 'pass357');

-- Insert into Car
INSERT INTO CAR (LINCESPLATE, Model, Year)
VALUES 
('ABC123', 'Toyota Camry', 2015),
('XYZ789', 'Honda Civic', 2018),
('DEF456', 'Ford Focus', 2017),
('GHI012', 'Hyundai Elantra', 2019),
('JKL345', 'Nissan Altima', 2016),
('MNO678', 'Chevrolet Cruze', 2020),
('PQR901', 'Kia Rio', 2014),
('STU234', 'Mazda 3', 2018),
('VWX567', 'Volkswagen Jetta', 2017),
('YZA890', 'Subaru Impreza', 2019),
('BCD123', 'BMW 3 Series', 2015),
('EFG456', 'Mercedes C-Class', 2016),
('HIJ789', 'Audi A4', 2018),
('KLM012', 'Lexus IS', 2017),
('NOP345', 'Infiniti Q50', 2019),
('QRS678', 'Acura TLX', 2020),
('TUV901', 'Volvo S60', 2016),
('WXY234', 'Tesla Model 3', 2018),
('ZAB567', 'Porsche 911', 2015),
('CDE890', 'Jaguar XE', 2017),
('FGH123', 'Land Rover Evoque', 2019),
('IJK456', 'Jeep Cherokee', 2016),
('LMN789', 'Dodge Charger', 2018),
('OPQ012', 'Chrysler 300', 2017),
('RST345', 'GMC Acadia', 2019),
('UVW678', 'Cadillac ATS', 2015),
('XYZ901', 'Lincoln MKZ', 2016),
('ABC234', 'Buick Regal', 2018),
('DEF567', 'Mitsubishi Lancer', 2017),
('GHI890', 'Suzuki Swift', 2019),
('JKL123', 'Peugeot 308', 2016),
('MNO456', 'Renault Megane', 2018),
('PQR789', 'Citroen C4', 2017),
('STU012', 'Fiat 500', 2019),
('VWX345', 'Alfa Romeo Giulia', 2016),
('YZA678', 'Maserati Ghibli', 2018),
('BCD901', 'Bentley Continental', 2015),
('EFG234', 'Rolls-Royce Ghost', 2017),
('HIJ567', 'Ferrari 488', 2019),
('KLM890', 'Lamborghini Huracan', 2016),
('NOP123', 'McLaren 720S', 2018),
('QRS456', 'Aston Martin DB11', 2017),
('TUV789', 'Bugatti Chiron', 2019),
('WXY012', 'Pagani Huayra', 2016),
('ZAB345', 'Koenigsegg Jesko', 2018),
('CDE678', 'Toyota Corolla', 2015),
('FGH901', 'Honda Accord', 2017),
('IJK234', 'Ford Mustang', 2019),
('LMN567', 'Hyundai Sonata', 2016),
('OPQ890', 'Nissan Maxima', 2018);

-- Insert into Owns
INSERT INTO OWNS (CustomerID, CarID)
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20),
(21, 21),
(22, 22),
(23, 23),
(24, 24),
(25, 25),
(26, 26),
(27, 27),
(28, 28),
(29, 29),
(30, 30),
(31, 31),
(32, 32),
(33, 33),
(34, 34),
(35, 35),
(36, 36),
(37, 37),
(38, 38),
(39, 39),
(40, 40),
(41, 41),
(42, 42),
(43, 43),
(44, 44),
(45, 45),
(46, 46),
(47, 47),
(48, 48),
(49, 49),
(50, 50);

-- Insert into Accident
INSERT INTO ACCIDENT (Date, Location, Description)
VALUES 
('2023-01-15', 'Cairo', 'Rear-end collision'),
('2023-02-20', 'Giza', 'Side-impact crash'),
('2023-03-10', 'Alexandria', 'Fender bender'),
('2023-04-05', 'Nasr City', 'Hit and run'),
('2023-05-12', 'Maadi', 'T-bone accident'),
('2023-06-18', 'Heliopolis', 'Multi-car pileup'),
('2023-07-22', 'Zamalek', 'Pedestrian collision'),
('2023-08-30', 'Dokki', 'Single-car crash'),
('2023-09-14', 'Mohandessin', 'Rollover accident'),
('2023-10-25', 'Shubra', 'Intersection collision'),
('2023-11-08', 'Imbaba', 'Side-swipe'),
('2023-12-19', 'Haram', 'Rear-end collision'),
('2024-01-07', 'Nasr City', 'Head-on crash'),
('2024-02-14', '6th Oct', 'Fender bender'),
('2024-03-22', 'Sheikh Zayed', 'T-bone accident'),
('2024-04-11', 'New Cairo', 'Multi-car pileup'),
('2024-05-09', 'Tagamoa', 'Pedestrian collision'),
('2024-06-17', 'Madinaty', 'Single-car crash'),
('2024-07-04', 'Rehab', 'Rollover accident'),
('2024-08-13', 'Obour', 'Intersection collision'),
('2024-09-21', 'Sahel', 'Side-swipe'),
('2024-10-30', 'Marina', 'Rear-end collision'),
('2024-11-15', 'Hurghada', 'Head-on crash'),
('2024-12-02', 'Sharm', 'Fender bender'),
('2025-01-10', 'Luxor', 'T-bone accident'),
('2025-02-18', 'Aswan', 'Multi-car pileup'),
('2025-03-27', 'Suez', 'Pedestrian collision'),
('2025-04-04', 'Ismailia', 'Single-car crash'),
('2025-05-12', 'Port Said', 'Rollover accident'),
('2025-06-20', 'Damietta', 'Intersection collision'),
('2025-07-08', 'Mansoura', 'Side-swipe'),
('2025-08-16', 'Tanta', 'Rear-end collision'),
('2025-09-24', 'Zagazig', 'Head-on crash'),
('2025-10-03', 'Banha', 'Fender bender'),
('2025-11-11', 'Shibin', 'T-bone accident'),
('2025-12-19', 'Menouf', 'Multi-car pileup'),
('2024-01-27', 'Kafr El Sheikh', 'Pedestrian collision'),
('2024-02-05', 'Damanhour', 'Single-car crash'),
('2024-03-14', 'Rosetta', 'Rollover accident'),
('2024-04-22', 'Edfu', 'Intersection collision'),
('2024-05-30', 'Kom Ombo', 'Side-swipe'),
('2024-06-08', 'Esna', 'Rear-end collision'),
('2024-07-16', 'Qena', 'Head-on crash'),
('2024-08-24', 'Sohag', 'Fender bender'),
('2024-09-02', 'Assiut', 'T-bone accident'),
('2024-10-11', 'Minya', 'Multi-car pileup'),
('2024-11-19', 'Beni Suef', 'Pedestrian collision'),
('2024-12-27', 'Fayoum', 'Single-car crash'),
('2025-01-05', 'Giza', 'Rollover accident'),
('2025-02-13', 'Cairo', 'Intersection collision');

-- Insert into InvolvedIn
INSERT INTO INVOLVEDIN (CarID, AccidentID)
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20),
(21, 21),
(22, 22),
(23, 23),
(24, 24),
(25, 25),
(26, 26),
(27, 27),
(28, 28),
(29, 29),
(30, 30),
(31, 31),
(32, 32),
(33, 33),
(34, 34),
(35, 35),
(36, 36),
(37, 37),
(38, 38),
(39, 39),
(40, 40),
(41, 41),
(42, 42),
(43, 43),
(44, 44),
(45, 45),
(46, 46),
(47, 47),
(48, 48),
(49, 49),
(50, 50);

-- Insert into RepairShop
INSERT INTO REPAIRSHOP (ShopName, Location, ContactNumber)
VALUES 
('Cairo Auto Repair', 'Downtown Cairo', '0123456789'),
('Giza Fix', 'Giza Square', '0119876543'),
('Alex Auto', 'Alexandria', '0101234567'),
('Nasr City Garage', 'Nasr City', '0159876543'),
('Maadi Motors', 'Maadi', '0128765432'),
('Heliopolis Repairs', 'Heliopolis', '0112345678'),
('Zamalek Auto', 'Zamalek', '0109876543'),
('Dokki Fix', 'Dokki', '0151234567'),
('Mohandessin Garage', 'Mohandessin', '0127654321'),
('Shubra Repairs', 'Shubra', '0118765432'),
('Imbaba Auto', 'Imbaba', '0103456789'),
('Haram Fix', 'Haram', '0156789012'),
('Nasr City Auto', 'Nasr City', '0125432109'),
('6th Oct Garage', '6th Oct', '0114321098'),
('Sheikh Zayed Repairs', 'Sheikh Zayed', '0109871234'),
('New Cairo Auto', 'New Cairo', '0153210987'),
('Tagamoa Fix', 'Tagamoa', '0121098765'),
('Madinaty Garage', 'Madinaty', '0118761234'),
('Rehab Repairs', 'Rehab', '0105432109'),
('Obour Auto', 'Obour', '0159873210'),
('Sahel Fix', 'Sahel', '0123459876'),
('Marina Garage', 'Marina', '0116789012'),
('Hurghada Repairs', 'Hurghada', '0101239876'),
('Sharm Auto', 'Sharm', '0154567890'),
('Luxor Fix', 'Luxor', '0127890123'),
('Aswan Garage', 'Aswan', '0112340987'),
('Suez Repairs', 'Suez', '0105678901'),
('Ismailia Auto', 'Ismailia', '0158901234'),
('Port Said Fix', 'Port Said', '0121234567'),
('Damietta Garage', 'Damietta', '0114567890'),
('Mansoura Repairs', 'Mansoura', '0107890123'),
('Tanta Auto', 'Tanta', '0150123456'),
('Zagazig Fix', 'Zagazig', '0122345678'),
('Banha Garage', 'Banha', '0115678901'),
('Shibin Repairs', 'Shibin', '0108901234'),
('Menouf Auto', 'Menouf', '0151234567'),
('Kafr El Sheikh Fix', 'Kafr El Sheikh', '0123456789'),
('Damanhour Garage', 'Damanhour', '0116789012'),
('Rosetta Repairs', 'Rosetta', '0109012345'),
('Edfu Auto', 'Edfu', '0152345678'),
('Kom Ombo Fix', 'Kom Ombo', '0125678901'),
('Esna Garage', 'Esna', '0117890123'),
('Qena Repairs', 'Qena', '0100123456'),
('Sohag Auto', 'Sohag', '0153456789'),
('Assiut Fix', 'Assiut', '0126789012'),
('Minya Garage', 'Minya', '0119012345'),
('Beni Suef Repairs', 'Beni Suef', '0102345678'),
('Fayoum Auto', 'Fayoum', '0154567890'),
('Giza Fix 2', 'Giza', '0127890123'),
('Cairo Auto 2', 'Cairo', '0110123456');

-- Insert into Repairs
INSERT INTO REPAIRS (ACCIDENTID, SHOPID)
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19),
(20, 20),
(21, 21),
(22, 22),
(23, 23),
(24, 24),
(25, 25),
(26, 26),
(27, 27),
(28, 28),
(29, 29),
(30, 30),
(31, 31),
(32, 32),
(33, 33),
(34, 34),
(35, 35),
(36, 36),
(37, 37),
(38, 38),
(39, 39),
(40, 40),
(41, 41),
(42, 42),
(43, 43),
(44, 44),
(45, 45),
(46, 46),
(47, 47),
(48, 48),
(49, 49),
(50, 50);

-- Insert into InsurancePolicy
INSERT INTO INSURANCEPOLICY (CarID, StartDate, EndDate, CoverageType)
VALUES 
(1, '2023-01-01', '2024-01-01', 'Collision Coverage'),
(2, '2023-02-01', '2024-02-01', 'Comprehensive Coverage'),
(3, '2023-03-01', '2024-03-01', 'Liability Coverage'),
(4, '2023-04-01', '2024-04-01', 'Collision Coverage'),
(5, '2023-05-01', '2024-05-01', 'Comprehensive Coverage'),
(6, '2023-06-01', '2024-06-01', 'Liability Coverage'),
(7, '2023-07-01', '2024-07-01', 'Collision Coverage'),
(8, '2023-08-01', '2024-08-01', 'Comprehensive Coverage'),
(9, '2023-09-01', '2024-09-01', 'Liability Coverage'),
(10, '2023-10-01', '2024-10-01', 'Collision Coverage'),
(11, '2023-11-01', '2024-11-01', 'Comprehensive Coverage'),
(12, '2023-12-01', '2024-12-01', 'Liability Coverage'),
(13, '2024-01-01', '2025-01-01', 'Collision Coverage'),
(14, '2024-02-01', '2025-02-01', 'Comprehensive Coverage'),
(15, '2024-03-01', '2025-03-01', 'Liability Coverage'),
(16, '2024-04-01', '2025-04-01', 'Collision Coverage'),
(17, '2024-05-01', '2025-05-01', 'Comprehensive Coverage'),
(18, '2024-06-01', '2025-06-01', 'Liability Coverage'),
(19, '2024-07-01', '2025-07-01', 'Collision Coverage'),
(20, '2024-08-01', '2025-08-01', 'Comprehensive Coverage'),
(21, '2024-09-01', '2025-09-01', 'Liability Coverage'),
(22, '2024-10-01', '2025-10-01', 'Collision Coverage'),
(23, '2024-11-01', '2025-11-01', 'Comprehensive Coverage'),
(24, '2024-12-01', '2025-12-01', 'Liability Coverage'),
(25, '2023-01-01', '2024-01-01', 'Collision Coverage'),
(26, '2023-02-01', '2024-02-01', 'Comprehensive Coverage'),
(27, '2023-03-01', '2024-03-01', 'Liability Coverage'),
(28, '2023-04-01', '2024-04-01', 'Collision Coverage'),
(29, '2023-05-01', '2024-05-01', 'Comprehensive Coverage'),
(30, '2023-06-01', '2024-06-01', 'Liability Coverage'),
(31, '2023-07-01', '2024-07-01', 'Collision Coverage'),
(32, '2023-08-01', '2024-08-01', 'Comprehensive Coverage'),
(33, '2023-09-01', '2024-09-01', 'Liability Coverage'),
(34, '2023-10-01', '2024-10-01', 'Collision Coverage'),
(35, '2023-11-01', '2024-11-01', 'Comprehensive Coverage'),
(36, '2023-12-01', '2024-12-01', 'Liability Coverage'),
(37, '2024-01-01', '2025-01-01', 'Collision Coverage'),
(38, '2024-02-01', '2025-02-01', 'Comprehensive Coverage'),
(39, '2024-03-01', '2025-03-01', 'Liability Coverage'),
(40, '2024-04-01', '2025-04-01', 'Collision Coverage'),
(41, '2024-05-01', '2025-05-01', 'Comprehensive Coverage'),
(42, '2024-06-01', '2025-06-01', 'Liability Coverage'),
(43, '2024-07-01', '2025-07-01', 'Collision Coverage'),
(44, '2024-08-01', '2025-08-01', 'Comprehensive Coverage'),
(45, '2024-09-01', '2025-09-01', 'Liability Coverage'),
(46, '2024-10-01', '2025-10-01', 'Collision Coverage'),
(47, '2024-11-01', '2025-11-01', 'Comprehensive Coverage'),
(48, '2024-12-01', '2025-12-01', 'Liability Coverage'),
(49, '2023-01-01', '2024-01-01', 'Collision Coverage'),
(50, '2023-02-01', '2024-02-01', 'Comprehensive Coverage');

-- Insert into Claim
INSERT INTO CLAIM (AccidentID, Amount, Status)
VALUES 
(1, 1000.00, 'Approved'),
(2, 1500.00, 'Pending'),
(3, 800.00, 'Rejected'),
(4, 1200.00, 'Approved'),
(5, 2000.00, 'Pending'),
(6, 900.00, 'Rejected'),
(7, 1100.00, 'Approved'),
(8, 1300.00, 'Pending'),
(9, 1600.00, 'Rejected'),
(10, 1400.00, 'Approved'),
(11, 1700.00, 'Pending'),
(12, 950.00, 'Rejected'),
(13, 1800.00, 'Approved'),
(14, 1050.00, 'Pending'),
(15, 1900.00, 'Rejected'),
(16, 1150.00, 'Approved'),
(17, 1250.00, 'Pending'),
(18, 1350.00, 'Rejected'),
(19, 1450.00, 'Approved'),
(20, 1550.00, 'Pending'),
(21, 1650.00, 'Rejected'),
(22, 1750.00, 'Approved'),
(23, 1850.00, 'Pending'),
(24, 1950.00, 'Rejected'),
(25, 1000.00, 'Approved'),
(26, 1500.00, 'Pending'),
(27, 800.00, 'Rejected'),
(28, 1200.00, 'Approved'),
(29, 2000.00, 'Pending'),
(30, 900.00, 'Rejected'),
(31, 1100.00, 'Approved'),
(32, 1300.00, 'Pending'),
(33, 1600.00, 'Rejected'),
(34, 1400.00, 'Approved'),
(35, 1700.00, 'Pending'),
(36, 950.00, 'Rejected'),
(37, 1800.00, 'Approved'),
(38, 1050.00, 'Pending'),
(39, 1900.00, 'Rejected'),
(40, 1150.00, 'Approved'),
(41, 1250.00, 'Pending'),
(42, 1350.00, 'Rejected'),
(43, 1450.00, 'Approved'),
(44, 1550.00, 'Pending'),
(45, 1650.00, 'Rejected'),
(46, 1750.00, 'Approved'),
(47, 1850.00, 'Pending'),
(48, 1950.00, 'Rejected'),
(49, 1000.00, 'Approved'),
(50, 1500.00, 'Pending');