﻿using EmployeeManagement.Models;
using EmployeeModels.Models;
using EmployeeRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IRepository repository;
        public EmployeeController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("api/addEmployee")]
        public IActionResult AddEmployee([FromBody] EmployeeModel employee)
        {
            var result = this.repository.CreateEmployee(employee);
            if (result.Equals("SUCCESS"))
            {
                return this.Ok(new { success = true, Message = "Data Added successfully", Data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, Message = "Data is Not Added Succesfully " });
            }
        }

        /// <summary>
        /// Logins to system.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/loginEmployee")]
        public IActionResult Login(string Email, string Password)
        {
            var result = this.repository.LoginToSystem(Email, Password);
            if (result.Equals("LOGIN SUCCESS"))
            {
                return this.Ok(result);
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getEmployee")]
        public IActionResult GetEmployees(string id)
        {
            try
            {
                IEnumerable<EmployeeModel> employeeList = this.repository.GetEmployee(id);
                return this.Ok(employeeList);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes the personal details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/deleteRecord/{id}/")]
        public IActionResult DeletePersonalDetails([FromRoute]int id)
        {
            var result = this.repository.DeleteEmployee(id);
            if (result.Equals("Employee Deleted"))
            {
                return this.Ok(result);
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Updates the personal details.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        
        [HttpGet]
        [Route("api/getById/{id}/")]
        public IActionResult GetEmployeeByIds(int id)
        {
            try
            {
                IEnumerable<EmployeeModel> user = this.repository.GetEmployeeBy_ID(id);
                return this.Ok(user);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("api/updateRecord")]
        public IActionResult UpdateEmployee(EmployeeModel update)
        {
            var result = this.repository.UpdateEmployee(update);
            if (result.Equals("SUCCESS"))
            {
                return this.Ok(new { success = true, Message = "Record Updated successfully", Data = result });
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Resets the passwords.
        /// </summary>
        /// <param name="oldpass">The oldpass.</param>
        /// <param name="newpass">The newpass.</param>
        /// <returns></returns>

        [HttpPut]
        [Route("api/reset/{OldPassword}/{NewPassword}/")]
        public IActionResult ResetPasswords(string OldPassword, string NewPassword)
        {
            var result = this.repository.ResetPassword(OldPassword, NewPassword);
            if (result.Equals("SUCCESS"))
            {
                return this.Ok(new { success = true, Message = "Password Updated successfully", Data = result });
            }
            else
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Send password on Email.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>

        [HttpGet]
        [Route("api/emailSend/{emailAddress}/")]
        public IActionResult SendPassword([FromRoute] string emailAddress)
        {
            var result = this.repository.SendEmail(emailAddress);
            if (result.Equals("SUCCESS"))
            {
                return this.Ok(new { success = true, Message = "Password Send Successfully", Data = result });
            }
            else
            {
                return this.BadRequest();
            }
        }
    }
}