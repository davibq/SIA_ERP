﻿ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [ERP_Finanzas], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

