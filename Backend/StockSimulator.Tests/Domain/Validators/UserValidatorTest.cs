using FluentValidation.TestHelper;
using NUnit.Framework;
using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Validators;

namespace StockSimulator.Tests.Domain.Validators
{
    [TestFixture]
    public class UserValidatorTest
    {
        private UserValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new UserValidator();
        }

        #region Related functions

        private void ValidateName(User model, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(u => u.Name);
            else
                result.ShouldNotHaveValidationErrorFor(u => u.Name);
        }

        private void ValidateEmail(User model, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(u => u.Email);
            else
                result.ShouldNotHaveValidationErrorFor(u => u.Email);
        }

        private void ValidatePassword(User model, bool shouldHaveError = false)
        {
            var result = validator.TestValidate(model);
            if (shouldHaveError)
                result.ShouldHaveValidationErrorFor(u => u.Password);
            else
                result.ShouldNotHaveValidationErrorFor(u => u.Password);

        }

        #endregion

        #region Name Validation

        [Test]
        public void Should_have_error_when_name_is_null()
        {
            var model = new User() { Name = null };
            ValidateName(model,true);
        }

        [Test]
        public void Should_have_error_when_name_is_empty()
        {
            var model = new User() { Name = string.Empty };
            ValidateName(model,true);
        }

        [Test]
        public void Should_have_error_when_name_is_lt_3()
        {
            var model = new User() { Name = "AB" };
            ValidateName(model,true);
        }

        [Test]
        public void Should_have_error_when_name_is_gt_100()
        {
            var name = "A".PadRight(101, 'a');
            var model = new User() { Name = name };
            ValidateName(model,true);
        }

        [Test]
        public void Should_have_pass_when_name_has_met_all_requirements()
        {
            var model = new User() { Name = "Gabriela de Souza Pereira" };
            ValidateName(model);
        }

        #endregion

        #region Email Validation

        [Test]
        public void Should_have_error_when_email_is_null()
        {
            var model = new User() { Email = null };
            ValidateEmail(model,true);
        }

        [Test]
        public void Should_have_error_when_email_is_empty()
        {
            var model = new User() { Email = "" };
            ValidateEmail(model,true);
        }

        [Test]
        public void Should_have_error_when_email_has_an_invalid_format()
        {
            var model = new User() { Email = "marcelo.castro" };
            ValidateEmail(model,true);
        }

        [Test]
        public void Should_have_a_valid_mail()
        {
            var model = new User() { Email = "marcelo.developer@outlook.com" };
            ValidateEmail(model);
        }

        #endregion

        #region Password Validation
        [Test]
        public void Should_have_error_when_password_is_null()
        {
            var model = new User() { Password = null };
            ValidatePassword(model, true);
        }

        [Test]
        public void Should_have_error_when_password_is_empty()
        {
            var model = new User() { Password = "" };
            ValidatePassword(model, true);
        }

        [Test]
        public void Should_have_error_when_password_is_lt_8_characters()
        {
            var model = new User() { Password = "258s596" };
            ValidatePassword(model, true);
        }

        [Test]
        public void Should_have_error_when_password_is_gt_20_characters()
        {
            var model = new User() { Password = "35s89z67s8xsdfr56s549" };
            ValidatePassword(model, true);
        }

        [Test]
        public void Should_have_pass_when_password_has_met_all_requirements()
        {
            var model = new User() { Password = "1253d58s9a12r456s8" };
            ValidatePassword(model);
        }

        #endregion
    }
}
