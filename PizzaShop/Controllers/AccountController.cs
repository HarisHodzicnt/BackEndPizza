using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PizzaShop.Model;
namespace PizzaShop.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {

        private readonly UserManager<ExtendedUser> _userModel;
        private readonly SignInManager<ExtendedUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ExtendedUser> userModel, SignInManager<ExtendedUser> signInManager, IConfiguration configuration)
        {
            _userModel = userModel;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<bool> CheckEmail(string email)
        {

            var user = await _userModel.FindByEmailAsync(email);

            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        [Route("login")]
        [HttpPost]
        public async Task<object> Login([FromBody]SignIn model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userModel.FindByEmailAsync(model.Email);
                if(user!=null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {

                        return Ok(new { data = model });

                    }
                    ModelState.AddModelError("error", result.Succeeded.ToString());

                }
                    


            }

            return BadRequest(new { message = "error", ModelState });

        }

        [Route("register")]
        public async Task<IActionResult> SignUp([FromBody]SignUp model)
        {

            if (ModelState.IsValid)
            {
                //bool isEmailValid = CheckEmail(model.Email).Result;

                //if (!isEmailValid)
                //{
                //    ModelState.AddModelError("", "This Email is already in use");
                //    return BadRequest(new { message = "error", ModelState });
                //}


                var user = new ExtendedUser() { Email = model.Email, UserName = model.Email, City = model.City, PhoneNumber=model.PhoneNumber };


                var response = await _userModel.CreateAsync(user, model.Password);

                if (response.Succeeded)
                {
                    if (_signInManager.IsSignedIn(User))
                    {
                        return Ok(model);

                    }
                    var registerUser = await _userModel.FindByEmailAsync(model.Email);
                   
                 
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    SignIn data = new SignIn() {Email=model.Email, Password=model.Password };
                    return Ok(new { data });
                }
                else
                {
                    foreach (var error in response.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            return BadRequest(new { message = "error", ModelState });
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("You are logged out ");
        }
    }
}