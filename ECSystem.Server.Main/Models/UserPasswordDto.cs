namespace ECSystem.Server.Main.Models {
    public record class UserPasswordDto {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;

    }
}
