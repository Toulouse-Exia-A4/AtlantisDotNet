﻿INSERT INTO [Admin] (AdminId, Password) VALUES ('AtlantisAdmin', 'pass');

INSERT INTO [DeviceType] (Type, Unit) VALUES ('Temperature', '°C');
INSERT INTO [DeviceType] (Type, Unit) VALUES ('Pression', 'bar');
INSERT INTO [DeviceType] (Type) VALUES ('Mouvement');

INSERT INTO [User] (UserId) VALUES ('aaaaaaaa');
INSERT INTO [User] (UserId) VALUES ('bbbbbbbb');

INSERT INTO [Device] (DeviceId, DeviceTypeId, UserId) VALUES ('XXXXXX', 0, 0);
INSERT INTO [Device] (DeviceId, DeviceTypeId) VALUES ('AAAAAA', 1);
INSERT INTO [Device] (DeviceId, DeviceTypeId) VALUES ('BBBBBB', 2);
INSERT INTO [Device] (DeviceId, DeviceTypeId, UserId) VALUES ('CCCCCC', 2, 1);