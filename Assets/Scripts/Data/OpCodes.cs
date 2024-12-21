namespace Data
{
    public static class OpCodes
    {
        public const long SpawnPlayers = 1;
        public const long PlayerState = 2;
        public const long Running = 3;
        public const long Falling = 4;
        public const long Jumping = 5;
        public const long Rolling = 6;
        public const long Attacking = 7;
        public const long Grounded = 8;
        public const long TakeDamage = 9;
        public const long Die = 10;
        public const long Health = 11;
        public const long Ready = 12;
        public const long Cancel = 13;
    }
}

//await socket.SendMatchStateAsync(match.Id, OpCodes.Position, JsonWriter.ToJson(state));
