CREATE TABLE [dbo].[Tile]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [GameSessionId] UNIQUEIDENTIFIER NOT NULL, 
    [Address] TEXT NOT NULL, 
    [PlayerId] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_TilePlayer] FOREIGN KEY (PlayerId) REFERENCES [Player]([Id]), 
    CONSTRAINT [FK_TileGameSession] FOREIGN KEY (GameSessionId) REFERENCES [GameSession]([Id]) 
)
