
USE [Teste]
GO

/****** Object:  StoredProcedure [dbo].[sp_Listar_Impostos_cfop]    Script Date: 28/06/2016 13:55:12 ******/
DROP PROCEDURE [dbo].[sp_Listar_Impostos_cfop]
GO

/****** Object:  StoredProcedure [dbo].[sp_Listar_Impostos_cfop]    Script Date: 28/06/2016 13:55:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Listar_Impostos_cfop]
AS
BEGIN
	SELECT 
		[CFOP], 
		SUM([BaseIcms]) 'Valor Total da Base de ICMS', 
		SUM([ValorIcms]) 'Valor Total do ICMS', 
		SUM([BaseIpi]) 'Valor Total da Base de IPI', 
		SUM([ValorIpi]) 'Valor Total do IPI'
	FROM 
		[dbo].[NotaFiscalItem] 
	GROUP BY 
		[CFOP] 
END

GO
--alteracao