
IF OBJECT_ID('dbo.P_NOTA_FISCAL') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.P_NOTA_FISCAL
    IF OBJECT_ID('dbo.P_NOTA_FISCAL') IS NOT NULL
        PRINT '<<< FALHA APAGANDO A PROCEDURE dbo.P_NOTA_FISCAL >>>'
    ELSE
        PRINT '<<< PROCEDURE dbo.P_NOTA_FISCAL APAGADA >>>'
END
go
SET QUOTED_IDENTIFIER ON
GO
SET NOCOUNT ON 
GO 
CREATE PROCEDURE dbo.P_NOTA_FISCAL 
(
	@pId int OUTPUT,
	@pNumeroNotaFiscal int,
	@pSerie int,
	@pNomeCliente varchar(50),
	@pEstadoDestino varchar(50),
	@pEstadoOrigem varchar(50)
)
AS
BEGIN
	


	
	IF (@pId = 0)
	BEGIN 
		INSERT INTO [dbo].[NotaFiscal]
           ([NumeroNotaFiscal]
           ,[Serie]
           ,[NomeCliente]
           ,[EstadoDestino]
           ,[EstadoOrigem])
		VALUES
           (@pNumeroNotaFiscal
           ,@pSerie
           ,@pNomeCliente
           ,@pEstadoDestino
           ,@pEstadoOrigem)

		SET @pId = @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE [dbo].[NotaFiscal]
		SET [NumeroNotaFiscal] = @pNumeroNotaFiscal
		  ,[Serie] = @pSerie
		  ,[NomeCliente] = @pNomeCliente
		  ,[EstadoDestino] = @pEstadoDestino
		  ,[EstadoOrigem] = @pEstadoOrigem
		WHERE Id = @pId
	END	
	  
END
GO
GRANT EXECUTE ON dbo.P_NOTA_FISCAL TO [public]
go
IF OBJECT_ID('dbo.P_NOTA_FISCAL') IS NOT NULL
    PRINT '<<< PROCEDURE dbo.P_NOTA_FISCAL CRIADA >>>'
ELSE
    PRINT '<<< FALHA NA CRIACAO DA PROCEDURE dbo.P_NOTA_FISCAL >>>'
go


