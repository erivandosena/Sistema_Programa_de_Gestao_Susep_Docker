USE [pgd_susep]
GO

-- Cria novo BD staging (testes/suporte)
RESTORE DATABASE [pgd_staging] FROM  DISK = N'/var/opt/mssql/data/pgd_susep.bak' WITH  FILE = 1,  MOVE N'pgd_susep' TO N'/var/opt/mssql/data/pgd_staging.mdf',  MOVE N'pgd_susep_log' TO N'/var/opt/mssql/data/pgd_staging_log.ldf',  NOUNLOAD,  REPLACE,  STATS = 5
GO

-- Cria novo BD treinamento/usuários
RESTORE DATABASE [pgd_train] FROM  DISK = N'/var/opt/mssql/data/pgd_susep.bak' WITH  FILE = 1,  MOVE N'pgd_susep' TO N'/var/opt/mssql/data/pgd_train.mdf',  MOVE N'pgd_susep_log' TO N'/var/opt/mssql/data/pgd_train_log.ldf',  NOUNLOAD,  REPLACE,  STATS = 5
GO