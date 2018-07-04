INSERT INTO [Admin] (AdminId, Password) VALUES ('AtlantisAdmin', '1a1dc91c907325c69271ddf0c944bc72');

INSERT INTO [User] (UserId, Firstname, Lastname) VALUES ('aaaaaaaa', 'Jean', 'Dupond');
INSERT INTO [User] (UserId, Firstname, Lastname) VALUES ('bbbbbbbb', 'Alfred', 'Polochon');

INSERT INTO [Device] (DeviceTypeId, UserId) VALUES (1, 1);
INSERT INTO [Device] (DeviceTypeId) VALUES (1);
INSERT INTO [Device] (DeviceTypeId) VALUES (2);
INSERT INTO [Device] (DeviceTypeId, UserId) VALUES (2, 1);
INSERT INTO [Device] (DeviceTypeId, UserId, Name) VALUES (2, 2, 'Device_D');