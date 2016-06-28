USE [Teste]
GO

/****** Object:  StoredProcedure [dbo].[P_NOTA_FISCAL_ITEM]    Script Date: 28/06/2016 15:29:23 ******/
DROP PROCEDURE [dbo].[P_NOTA_FISCAL_ITEM]
GO

/****** Object:  StoredProcedure [dbo].[P_NOTA_FISCAL_ITEM]    Script Date: 28/06/2016 15:29:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[P_NOTA_FISCAL_ITEM]
(
	@pId int OUTPUT,
    @pIdNotaFiscal int,
    @pCfop varchar(5),
    @pTipoIcms varchar(20),
    @pBaseIcms decimal(18,5),
    @pAliquotaIcms decimal(18,5),
    @pValorIcms decimal(18,5),
    @pNomeProduto varchar(50),
    @pCodigoProduto varchar(20),
	@pBaseIpi  decimal(18,5),
	@pAliquotaIpi decimal(18,5),
	@pValorIpi  decimal(18,5),
	@pDesconto decimal(18,5)

)
AS
BEGIN
	IF (@pId = 0)
	BEGIN 		
		INSERT INTO [dbo].[NotaFiscalItem]
           ([IdNotaFiscal]
           ,[Cfop]
           ,[TipoIcms]
           ,[BaseIcms]
           ,[AliquotaIcms]
           ,[ValorIcms]
           ,[NomeProduto]
           ,[CodigoProduto]
		   ,[BaseIpi]
		   ,[AliquotaIpi]
		   ,[ValorIpi]
		   ,[Desconto])
		VALUES
           (@pIdNotaFiscal,
			@pCfop,
			@pTipoIcms,
			@pBaseIcms,
			@pAliquotaIcms,
			@pValorIcms,
			@pNomeProduto,
			@pCodigoProduto,
			@pBaseIpi,
			@pAliquotaIpi,
			@pValorIpi ,
			@pDesconto)

		SET @pId = @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE [dbo].[NotaFiscalItem]
		SET [IdNotaFiscal] = @pIdNotaFiscal
			,[Cfop] = @pCfop
			,[TipoIcms] = @pTipoIcms
			,[BaseIcms] = @pBaseIcms
			,[AliquotaIcms] = @pAliquotaIcms
			,[ValorIcms] = @pValorIcms
			,[NomeProduto] = @pNomeProduto
			,[CodigoProduto] = @pCodigoProduto
		 WHERE Id = @pId
	END	    
END

GO

