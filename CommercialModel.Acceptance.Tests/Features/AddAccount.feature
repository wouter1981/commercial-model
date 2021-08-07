Feature: Adding an account to the commercial model
	This feature describes how to add an account to the commercial model

Background:
    Given the commercial model has been cleaned up

Scenario: Add a new account
	When a user adds the account 'test-account-setup'
    Then the commercial model has the account 'test-account-setup'

Scenario: Add a duplicate account fails
	When a user adds the account 'test-account-setup'
	When a user adds the account 'test-account-setup'
    Then adding the account fails