INSERT INTO [Admin] (AdminId, Password) VALUES ('AtlantisAdmin', 'pass');

INSERT INTO [User] (UserId, Firstname, Lastname) VALUES ('aaaaaaaa', 'Jean', 'Dupond');
INSERT INTO [User] (UserId, Firstname, Lastname) VALUES ('bbbbbbbb', 'Alfred', 'Polochon');

INSERT INTO [Device] (DeviceTypeId, UserId) VALUES (1, 1);
INSERT INTO [Device] (DeviceTypeId) VALUES (1);
INSERT INTO [Device] (DeviceTypeId) VALUES (2);
INSERT INTO [Device] (DeviceTypeId, UserId) VALUES (2, 1);
INSERT INTO [Device] (DeviceTypeId, UserId, Name) VALUES (2, 2, 'Device_D');