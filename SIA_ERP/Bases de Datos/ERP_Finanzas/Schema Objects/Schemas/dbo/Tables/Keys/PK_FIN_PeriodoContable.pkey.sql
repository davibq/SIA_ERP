﻿ALTER TABLE [dbo].[FIN_PeriodoContable]
    ADD CONSTRAINT [PK_FIN_PeriodoContable] PRIMARY KEY CLUSTERED ([IdPeriodoContable] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
