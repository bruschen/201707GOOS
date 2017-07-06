﻿@Web
Feature: BudgetQuery

@CleanTableBudgets
Scenario: Query budget within single month UI
        Given go to budget query page
        And Budget table existed budgets
        | Amount | YearMonth |
        | 30000  | 2017-04   |
        When Query from "2017-04-05" to "2017-04-14"
        Then show budget 10000.00


@CleanTableBudgets
Scenario: Query budget within single month
        Given Budget table existed budgets
        | Amount | YearMonth |
        | 60000  | 2017-04   |
        When query
        | StartDate  | EndDate    |
        | 2017-04-05 | 2017-04-14 |
        Then ViewData.Model should be
        | StartDate  | EndDate    | Amount |
        | 2017-04-05 | 2017-04-14 | 20000  |
