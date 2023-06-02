﻿using Blazor4.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blazor4.Service
{
    public class SearchService
    {
        private AdventureWorks2019Context _db;

        public string? SearchInput { get; set; }
        public Person? SelectedPerson { get; set; }
        public List<Person> SearchResults { get; set; }
        public EmployeeDepartmentHistory? SelectedPersonHistory { get; set; }
        public EmployeePayHistory? SelectedEmployeePayHistory { get; set; }
        public Blazor4.Models.Address? SelectedAddress { get; set; }
        public List<Shift> Shifts { get; set; }

        public event Action<Person> OnUserSelected;
        public List<JobCandidate> JobCandidates { get; set; }



        public SearchService(AdventureWorks2019Context dbContext)
        {
            _db = dbContext;
            SearchResults = new List<Person>();
            Shifts = new List<Shift>();
        }

        public void Initialize()
        {
            PerformSearch(); // Perform any initialization logic here
        }

        public void PerformSearch()
        {
            if (string.IsNullOrEmpty(SearchInput))
            {
                // Display all employees in alphabetical order by first name
                SearchResults = _db.Employee
                    .Include(e => e.BusinessEntity)
                    .Select(e => e.BusinessEntity)
                    .OrderBy(e => e.FirstName)
                    .ToList();
            }
            else
            {
                string[] names = SearchInput.Split(' ');

                string firstName = names[0];
                string lastName = names.Length > 1 ? names[1] : string.Empty;

                if (string.IsNullOrEmpty(lastName))
                {
                    SearchResults = _db.Employee
                        .Include(e => e.BusinessEntity)
                        .AsEnumerable()
                        .Where(e => e.BusinessEntity.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                    e.BusinessEntity.LastName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0)
                        .Select(e => e.BusinessEntity)
                        .OrderBy(e => e.FirstName)
                        .ToList();
                }
                else
                {
                    SearchResults = _db.Employee
                        .Include(e => e.BusinessEntity)
                        .AsEnumerable()
                        .Where(e => e.BusinessEntity.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0 &&
                                    e.BusinessEntity.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0)
                        .Select(e => e.BusinessEntity)
                        .OrderBy(e => e.FirstName)
                        .ToList();
                }
            }

            if (SearchResults.Count == 0)
            {
                // If no search results, display all employees
                SearchResults = _db.Employee
                    .Include(e => e.BusinessEntity)
                    .Select(e => e.BusinessEntity)
                    .OrderBy(e => e.FirstName)
                    .ToList();
            }

            SelectedPerson = null;
        }


        public void SelectUser(Person person)
        {
            var employee = _db.Employee.FirstOrDefault(e => e.BusinessEntityId == person.BusinessEntityId);
            if (employee != null)
            {
                SelectedPerson = employee.BusinessEntity;
                SelectedPersonHistory = GetEmployeeDepartmentHistory(employee.BusinessEntityId);

                // Update the shifts only if the selected person has changed
                if (SelectedPerson != person)
                {
                    Shifts = GetShiftsForBusinessEntity(person.BusinessEntityId);
                    SelectedPerson = person;
                }

                // Trigger the OnUserSelected event
                OnUserSelected?.Invoke(person);
            }
        }





        public EmployeeDepartmentHistory? GetEmployeeDepartmentHistory(int businessEntityId)
        {
            EmployeeDepartmentHistory employeeDepartmentHistory = _db.EmployeeDepartmentHistories
                .FirstOrDefault(edh => edh.BusinessEntityId == businessEntityId);

            if (employeeDepartmentHistory != null)
            {
                Department department = _db.Departments
                    .FirstOrDefault(d => d.DepartmentId == employeeDepartmentHistory.DepartmentId);

                if (department != null)
                {
                    employeeDepartmentHistory.Department = department;
                }
            }

            return employeeDepartmentHistory;
        }




        public EmployeePayHistory? GetEmployeePayHistory(int businessEntityId)
        {
            return _db.EmployeePayHistories.FirstOrDefault(p => p.BusinessEntityId == businessEntityId);
        }

        public Blazor4.Models.Address? GetAddressForBusinessEntity(int businessEntityId)
        {
            return _db.BusinessEntityAddresses
                .Include(bea => bea.Address)
                .FirstOrDefault(bea => bea.BusinessEntityId == businessEntityId)?.Address;
        }

        public List<Shift> GetShiftsForBusinessEntity(int businessEntityId)
        {
            return _db.EmployeeDepartmentHistories
                .Where(edh => edh.BusinessEntityId == businessEntityId)
                .Select(edh => edh.Shift)
                .ToList();
        }

    }
}
