USE [Teste]
GO

/****** Object:  StoredProcedure [dbo].[AGRUPA_CFOP]    Script Date: 06/27/2016 15:20:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AGRUPA_CFOP] 	
	@pNfeSerie int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT nfi.cfop, 
		SUM(nfi.BaseIcms) as VALOR_TOTAL_BASE_ICMS, 
		SUM(nfi.ValorIcms) AS VALOR_TOTAL_ICMS,
		SUM(nfi.BaseIpi) AS VALOR_TOTAL_BASE_IPI, 
		SUM(nfi.ValorIpi) AS VALOR_TOTAL_IPI
	FROM NotaFiscal nf inner join NotaFiscalItem nfi on nf.id = nfi.idNotaFiscal 
	where serie = @pNfeSerie 
	group by nfi.cfop
END

GO

