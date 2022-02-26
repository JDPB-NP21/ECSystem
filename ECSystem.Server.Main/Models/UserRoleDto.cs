namespace ECSystem.Server.Main.Models {
    public record class UserRoleDto {
        public string UserName { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
