Feature: Kill Publishers Queue
    As a developer
    I want to reset the publishers queue
    So that the system can reinitialize its state

    Background:
        Given I am subscribed to the "scan" topic

    @smoke
    Scenario: Kill publishers queue sends initialization message
        When I call KillPublishersQueue
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "initialization"

    Scenario: Kill publishers queue uses scan topic
        When I call KillPublishersQueue
        Then the subscriber should receive a message within 5 seconds
        And the message should be received on the "scan" topic