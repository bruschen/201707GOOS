@Web
Feature: BudgetQuery

@CleanTableBudgets
Scenario: Query Budget within single month
	Given go to budget query page
	And Budget table existed budget
	| Amount | YearMonth |
    | 30000  | 2017-04   |
	When Query from "2017-04-05" to "2017-04-14"
	Then show budget 10000.00
