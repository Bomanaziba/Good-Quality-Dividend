namespace GoodQualityDividend.Services
{
    public class SalaryTaxCalculation
    {
        /// <summary>
        /// Generates the pay.
        /// </summary>
        /// <param name="monthCode"></param>
        /// <param name="yearId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// companyId
        /// or
        /// monthCode
        /// or
        /// yearId
        /// or
        /// userInfo
        /// or
        /// company
        /// </exception>
        /* public string GeneratePay(string monthCode, int yearId)
        {

            //get login user information
            var userInfo = this.usersRepository.GetUserById((int)session.GetSessionValue(SessionKey.UserId));

            var companyId = (int)session.GetSessionValue(SessionKey.UserId);

            //check if companyId is greater or equal to zero
            if (companyId <= 0)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            
            //check if month code is equal to null
            if (monthCode == null)
            {
                throw new ArgumentNullException(nameof(monthCode));
            }

            //check if yearId id greater than or equal to zero 
            if (yearId <= 0)
            {
                throw new ArgumentNullException(nameof(yearId));
            }

            //get company information
            var company = this.companyRepository.GetCompanyById(companyId);
            
            string result = string.Empty;

            //check if user information is null
            if (userInfo == null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }

            //check if company information is null
            if(company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            
            //check if payroll history for a partiicular company exist
            var isDataOkay = (this.payrollRepository.GetPayrllHistoryByCompanyMonthYear(companyId, monthCode, yearId) == null) ? true : false;


            if (!isDataOkay)
            {
                return Messages.PayrollAlreadyExist;
            }
            
            if (isDataOkay)
            {

                //Get the list of employee for a particular company. Can not be null for payroll generation to occur
                var employeeBycompanyCollection = this.lookupRepository.GetEmployeeByCompanyId(company.CompanyId);
                
                var isEmployeeeRecordExist = (employeeBycompanyCollection == null || employeeBycompanyCollection.Count == 0) ? false : true;

                if (!isEmployeeeRecordExist)
                {
                    result = Messages.NoEmployeeRecord;

                    return result;
                }

                int payrollHistoryId = 0;

                var payrollHistory = new PayrollHistoryView();

                //Passing the values for PayrollHistory 
                payrollHistory.CompanyId = company.CompanyId;
                payrollHistory.Year = yearId;
                payrollHistory.MonthCode = monthCode;

                //Save the Payroll history
                result = this.payrollRepository.SavePayrollHistoryInfo(payrollHistory, out payrollHistoryId);

                if (string.IsNullOrEmpty(result))
                {

                    //loop through each employee in the company
                    foreach (var item in employeeBycompanyCollection)
                    {

                        PayrollView payroll = new PayrollView();

                        var payrollEmployeeDeductionCollection = new List<IPayrollEmployeeDeduction>();
                        var payrollEmployeeLoanCollection = new List<IPayrollEmployeeLoan>();
                        var payrollEmployeeRewardCollection = new List<IPayrollEmployeeReward>();

                        //Initiating value to calculate
                        decimal basicSalary = 0;
                        decimal totalBenefit = 0;
                        decimal benefitTaxable = 0;
                        decimal benefitNotTaxable = 0;
                        decimal totalBenefitTaxable = 0;
                        decimal totalBenefitNotTaxable = 0;
                        decimal rewardAmount = 0;
                        decimal deductionAmount = 0;
                        decimal monthlyPayment = 0;
                        decimal pensionContribution = 0;
                        decimal NetPay = 0;


                        //Get all reward for an employee
                        var rewardCollection = this.lookupRepository.GetEmployeeRewardByEmployeeId(item.EmployeeId);

                        //Get all loans for an employee
                        var loanCollection = this.lookupRepository.GetEmployeeLoanByEmployeeId(item.EmployeeId);
                        
                        //Get deduction of an employee
                        var employeeDeduction = this.employeeDeductionRepository.GetEmployeeDeductionByEmployeeId(item.EmployeeId);

                        //Get PayScale information
                        var basePay = this.levelGradeRepository.GetLevelGradeByLevelIdAndGradeId(item.CompanyId, item.LevelId, item.GradeId);

                        //Collatiion of Base Pay and monetary benefits for an employee
                        basicSalary = basePay.BasePay;

                        //Get the benefit(s) of an employee
                        var benefitCollections = this.levelGradeRepository.GetIPayScaleBenefitByPayScaleId(basePay.PayScaleId);

                        //Check if basePay null
                        var isSalaryRecordExist = (basePay == null) ? false : true;
                        
                        if (!isSalaryRecordExist)
                        {
                            result = Messages.levelRecordExist;

                            return result;
                        }

                        //Collation of Benefits for an employee 
                        foreach (var benefits in benefitCollections)
                        {

                            if (benefits != null)
                            {
                                decimal benefit = 0;

                                //Taxable benefits
                                if (benefits.IsTaxable)
                                {

                                    //Check if its Percentage of Base Pay
                                    if (benefits.PercentageofBase <= 0)
                                    {
                                        benefit = benefits.CashValue/12;

                                        benefitTaxable = (decimal)benefits.CashValue;

                                    }

                                    //Check if its Cash Values
                                    if (benefits.CashValue <= 0)
                                    {
                                        var benefitPercent = benefits.PercentageofBase;

                                        benefit = ((decimal)(basicSalary * benefitPercent) / 100)/12;

                                        benefitTaxable = (decimal)(basicSalary * benefitPercent) / 100;

                                    }

                                }
                                //Non Taxable benefits
                                else
                                {
                                    //Check if its Percentage of Base Pay
                                    if (benefits.PercentageofBase <= 0)
                                    {
                                        benefit = benefits.CashValue/12;

                                        benefitNotTaxable = benefits.CashValue;

                                    }

                                    //Check if its Cash Values
                                    if (benefits.CashValue <= 0)
                                    {
                                        var benefitPercent = benefits.PercentageofBase;

                                        benefit = ((decimal)(basicSalary * benefitPercent) / 100)/12;

                                        benefitNotTaxable = (decimal)(basicSalary * benefitPercent) / 100;

                                    }
                                    
                                }

                                totalBenefit += benefit;
                                totalBenefitTaxable += benefitTaxable;
                                totalBenefitNotTaxable += benefitNotTaxable;
                            }
                        }

                        //Collation of Rewards for an employee 
                        foreach (var reward in rewardCollection)
                        {

                            var payrollEmployeeReward = new PayrollEmployeeRewardModel();

                            if (reward.IsActive)
                            {
                                payroll.EmployeeRewardId = reward.RewardId;
                                rewardAmount += reward.Amount;
                                
                                payrollEmployeeReward.EmployeeRewardId = reward.EmployeeRewardId;
                                payrollEmployeeReward.CompanyId = item.CompanyId;

                                payrollEmployeeRewardCollection.Add(payrollEmployeeReward);
                            }
                        }

                        //Collation of deductioon for an employee
                        foreach (var deduction in employeeDeduction)
                        {

                            var payrollEmployeeDeduction = new PayrollEmployeeDeductionModel();

                            if (deduction.IsActive)
                            {
                                payroll.EmployeeDeductionId = deduction.DeductionId;
                                deductionAmount += deduction.DeductionAmount;

                                
                                    payrollEmployeeDeduction.EmployeeDeductionId = deduction.DeductionId;
                                    payrollEmployeeDeduction.CompanyId = item.CompanyId;

                                payrollEmployeeDeductionCollection.Add(payrollEmployeeDeduction);
                                
                            }
                        }

                        //Collation and Calculation of Month Installmental payment for loans collected by an employee
                        foreach (var loan in loanCollection)
                        {

                            var payrollEmployeeLoan = new PayrollEmployeeLoanModel();

                            //Check if the loan is active
                            if (loan.IsActive)
                            {

                                double principal = (double)loan.Amount;

                                double interestRate = (double)(loan.InterestRate / 100);
                                
                                int NoOfInstallation = loan.Tenure;
                                
                                if (loan.PeriodRemain > 0)
                                {
                                    monthlyPayment += (decimal)((principal * interestRate * (Math.Pow((1 + interestRate), NoOfInstallation))) / ((1 + interestRate)));
                                    --loan.PeriodRemain;
                                    payroll.EmployeeLoanId = loan.EmployeeLoanId;
                                    
                                        payrollEmployeeLoan.EmployeeLoanId = loan.EmployeeLoanId;
                                        payrollEmployeeLoan.CompanyId = item.CompanyId;

                                    payrollEmployeeLoanCollection.Add(payrollEmployeeLoan);

                                    //Update employeeloan after a payroll payment period
                                    loanRepository.UpdateLoanInfo(loan);
                                }
                                else
                                {
                                    //Delete Loan when periodRemaining is zero
                                    loanRepository.DeleteLoanInfo(loan.EmployeeLoanId);
                                }


                            }


                        }


                        //Get Number of Children
                        var childrenCollection = this.lookupRepository.GetChildrenInformationListById(item.EmployeeId);
                        var childrenConsolidation = 0;

                        //Get Spouse(s)
                        var spouseCollection = this.employeeRepository.GetSpouseInfoById(item.EmployeeId);
                        var spouseConsolidation = 0;

                        //Children Dependence Allowance in tax (with a maximum of four(4) children, with a constant value of NGN2500 per child
                        if (childrenCollection != null && childrenCollection.Count <= 4 && childrenCollection.Count >= 1)
                        {
                            childrenConsolidation = childrenCollection.Count * 2500;
                        }

                        //Spouse(s) Dependence Allowance in tax (with a maximum of four(2) spouse, with a constant value of NGN2000 per spouse (dependant)
                        if (spouseCollection != null && spouseCollection.Count <= 2 && spouseCollection.Count >= 1)
                        {
                            spouseConsolidation = spouseCollection.Count * 2000;
                        }

                        decimal totalPackage = basicSalary + totalBenefitTaxable + totalBenefitNotTaxable;

                        //Tax Consolidation Relief Allowance. Check Annual income s
                        decimal consolidationReliefAllowance = (totalPackage < 20000000) ? ((20 * totalPackage)/100 + 200000) : (21 * totalPackage)/100 ;
                        

                        //Collate Employee Pension Calculation
                        pensionContribution = totalPackage * (decimal)(7.5 / 100);

                        //Get tax list 
                        var taxCollection = this.lookupRepository.GetAllTax();
                        
                        //Get Taxable income
                        var taxableIncome = totalPackage - spouseConsolidation - childrenConsolidation - consolidationReliefAllowance - pensionContribution;



                        decimal taxValue = 0;

                        //Calculate tax base on the taxable income and Annual income
                        foreach (var tax in taxCollection)
                        {

                            if (taxableIncome < tax.AnnualIncome)
                            {
                                taxValue += taxableIncome * (tax.TaxRate / 100);
                                break;
                            }
                            else
                            {
                                taxValue += tax.AnnualIncome * (tax.TaxRate / 100);
                            }

                            taxableIncome = taxableIncome - tax.AnnualIncome;
                        }


                        if (taxValue < 0) taxValue = 0;


                        //Calualte the net pay
                        NetPay = (basicSalary / 12) + totalBenefit - monthlyPayment - deductionAmount + rewardAmount - (taxValue / 12) - (pensionContribution/12);

                        //Passing Value to the payroll 
                        payroll.BasicSalary = (basicSalary / 12);
                        payroll.CompanyId = company.CompanyId;
                        payroll.EmployeeId = item.EmployeeId;
                        payroll.NetSalary = NetPay;
                        payroll.YearId = yearId;
                        payroll.MonthCode = monthCode;
                        payroll.PayrollHistoryId = payrollHistoryId;

                        int payrollId = 0;

                        result = this.payrollRepository.SavePayroll(payroll, out payrollId);

                        //Adding all the deduction of a payroll to payroll deduction table
                        foreach (var cells in payrollEmployeeDeductionCollection)
                        {
                            cells.PayrollId = payrollId;
                            payrollRepository.SavePayrollEmployeeDeduction(cells);
                        }

                        //Adding all the loan of a payroll to payroll loan table
                        foreach (var packets in payrollEmployeeLoanCollection)
                        {
                            packets.PayrollId = payrollId;
                            payrollRepository.SavePayrollEmployeeLoan(packets);
                        }

                        //Adding all the rewards of a payroll to payroll reward table
                        foreach (var boxes in payrollEmployeeRewardCollection)
                        {
                            boxes.PayrollId = payrollId;
                            payrollRepository.SavePayrollEmployeeReward(boxes);
                        }

                    }
                }
               
            }
            
            return result;
        }
     */
    }
}