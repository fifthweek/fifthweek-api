# Updating Validation Behaviour

Ensure the following 2 tenets when updating validation behaviour:

1.  When reading data, validate against the lowest common denominator of all rules that have existed.

2.  When writing data, validate against the most recent rules.

## Introducing new types for new rulesets

If the new rule is less strict than the existing rule, then update the existing type with the new rules.

If the new rule is more string that the existing rule:

1.  Create a new `NewType` type (where 'Type' is the original type name).

2.  Extend the original type and apply its behaviours.

3.  Add the new (stricter) validation rules to the new type.

4.  Update commands that use said type for writing purposes to use the `NewType`.

    All reading operations should continue using the original `Type`.
