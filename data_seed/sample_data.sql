INSERT INTO DEPARTMENT (DepID, DepName) VALUES
  (1, 'Cardiology'),
  (2, 'Neurology');

INSERT INTO DOCTOR (ID, Name, Type, Mobile, Email, Gender, Fees, Salary) VALUES
  (101, 'Dr Rao', 'Consultant', '9876543210', 'rao@medibuddy.local', 'M', 700.0, 90000.0),
  (102, 'Dr Priya', 'Surgeon', '9876501234', 'priya@medibuddy.local', 'F', 900.0, 120000.0);

INSERT INTO NURSE (ID, Name, Mobile, Email, Gender, Salary) VALUES
  (201, 'Nurse Meera', '9000000001', 'meera@medibuddy.local', 'F', 40000.0),
  (202, 'Nurse Aman', '9000000002', 'aman@medibuddy.local', 'M', 42000.0);

INSERT INTO PATIENT (PID, FirstName, MidName, LastName, Mobile, Email, Address, Gender, DOB) VALUES
  (1001, 'Arun', 'K', 'Sharma', '8000000001', 'arun@example.com', 'Bengaluru', 'M', '1990-05-01'),
  (1002, 'Neha', NULL, 'Iyer', '8000000002', 'neha@example.com', 'Mysuru', 'F', '1995-09-10');

INSERT INTO TEST (ID, Name, Price) VALUES
  (1, 'Blood Test', 500),
  (2, 'MRI', 3000);

INSERT INTO MEDICINE (ID, Name, Price) VALUES
  (1, 'Paracetamol', 20),
  (2, 'Antibiotic', 120);

INSERT INTO WARD (ID, DepID, RoomSpecialCapacity, RoomSharedCapacity, RoomGeneralCapacity) VALUES
  (1, 1, 10, 20, 30),
  (2, 2, 8, 16, 24);

INSERT INTO ROOM (ID, WardID, Type, Rate, CurrentBedCapacity, MaxBedCapacity) VALUES
  (1, 1, 'Special', 2500.0, 5, 10),
  (2, 2, 'Shared', 1500.0, 6, 12);

INSERT INTO OPDBILLING (ID, PID, DocID) VALUES
  (1, 1001, 101),
  (2, 1002, 102);

INSERT INTO OPDTEST (OPDBillingID, TestID) VALUES
  (1, 1),
  (2, 2);

INSERT INTO OPDMEDICINE (OPDBillingID, MedicineID) VALUES
  (1, 1),
  (2, 2);

INSERT INTO OPDPATIENT (ID, PID, DocID, VisitDate, OPDBillingID, Discharged) VALUES
  (1, 1001, 101, '2026-01-15', 1, 0),
  (2, 1002, 102, '2026-01-16', 2, 1);

INSERT INTO IPDPATIENT (ID, PID, DocID, NurseID, EntryDate, ExitDate, RoomID, Discharged) VALUES
  (1, 1001, 101, 201, '2026-01-10', '2026-01-14', 1, 1),
  (2, 1002, 102, 202, '2026-01-11', '2026-01-18', 2, 0);

INSERT INTO IPDTEST (IPDPatientID, TestID) VALUES
  (1, 1),
  (2, 2);

INSERT INTO IPDMEDICINE (IPDPatientID, MedicineID) VALUES
  (1, 1),
  (2, 2);
