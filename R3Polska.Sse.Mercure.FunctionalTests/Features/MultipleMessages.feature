Feature: Multiple Messages Publishing
    As a developer
    I want to publish multiple messages in sequence
    So that I can verify the message ordering and delivery

    Background:
        Given I am subscribed to the "scan" topic

    Scenario: Publish multiple state transitions
        When I publish a ReadyPayload message to the "scan" topic
        And I publish a ProductDetectionPayload message with EAN "1234567890123" to the "scan" topic
        And I publish an AwaitsDropPayload message with EAN "1234567890123" to the "scan" topic
        And I publish a FinishedPayload message to the "scan" topic
        Then the subscriber should receive 4 messages within 10 seconds
        And the messages should be received in order

    Scenario: Publish service mode transition
        When I publish a ServiceEnterPayload message to the "scan" topic
        And I publish a ServiceExitPayload message to the "scan" topic
        Then the subscriber should receive 2 messages within 10 seconds