using Music.API.Entities.Commands;
using System;

namespace Music.API.Services
{
    public static class CommandValidator
    {
        public static ValidationResult Validate(ReleaseCreateCommand cmd)
        {
            if(cmd != null && !string.IsNullOrEmpty(cmd.Artist)
                               && !string.IsNullOrEmpty(cmd.Genre)
                               && !string.IsNullOrEmpty(cmd.Title))
            {
                return ValidationResult.Ok;
            }

            return ValidationResult.ErrorOf("Release should have non empty fields");
        }

        public static ValidationResult Validate(ReleaseUpdateCommand cmd)
        {
            if(!string.IsNullOrEmpty(cmd.Title)
                    || !string.IsNullOrEmpty(cmd.Genre)
                    || !string.IsNullOrEmpty(cmd.Artist)
                    || cmd.Cover != null)
            {
                return ValidationResult.Ok;
            }
            return ValidationResult.ErrorOf("Command has to update at least one field");
        }

        public static ValidationResult Validate(MetadataCreateCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.TrackId) || string.IsNullOrEmpty(command.ReleaseId))
            {
                return ValidationResult.ErrorOf("Command can't be null and should have track and release ids filled");
            }
            if (!string.IsNullOrEmpty(command.Title)
                    || !string.IsNullOrEmpty(command.Genre)
                    || !string.IsNullOrEmpty(command.Artist)
                    || !string.IsNullOrEmpty(command.Album)
                    || command.Number.HasValue)
            {
                return ValidationResult.Ok;
            }
            return ValidationResult.ErrorOf("Command has to initialize at least one field");
        }

        public static ValidationResult Validate(MetadataUpdateCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.TrackId) || string.IsNullOrEmpty(command.ReleaseId))
            {
                return ValidationResult.ErrorOf("Command can't be null and should have track and release ids filled");
            }
            if(!string.IsNullOrEmpty(command.Title)
                    || !string.IsNullOrEmpty(command.Genre)
                    || !string.IsNullOrEmpty(command.Artist)
                    || !string.IsNullOrEmpty(command.Album)
                    || command.Number.HasValue)
            {
                return ValidationResult.Ok;
            }
            return ValidationResult.ErrorOf("Command has to update at least one field");
        }

        public static ValidationResult Validate(TrackCreateCommand cmd)
        {
            if(cmd != null && cmd.Binary != null) { return ValidationResult.Ok; }
            return ValidationResult.ErrorOf("Command has to have binary not null");
        }

        public static ValidationResult Validate(TrackUpdateCommand cmd)
        {
            if (cmd != null && cmd.Binary != null) { return ValidationResult.Ok; }
            return ValidationResult.ErrorOf("Command has to have binary not null");
        }

        public static ValidationResult Validate(SubscriptionCreateCommand command)
        {
            if(command != null && !string.IsNullOrEmpty(command.ReleaseId) && command.UtcExpiration > DateTime.UtcNow)
            {
                return ValidationResult.Ok;
            }
            return ValidationResult.ErrorOf("Command has to have release id specified and have expiration date greater than UTC Now");
        }

        public static ValidationResult Validate(SubscriptionReplaceCommand command)
        {
            if (command != null && !string.IsNullOrEmpty(command.ReleaseId) && command.UtcExpiration > DateTime.UtcNow)
            {
                return ValidationResult.Ok;
            }
            return ValidationResult.ErrorOf("Command has to have release id specified and have expiration date greater than UTC Now");
        }
    }
}
