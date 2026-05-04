# Visual UML Diagram for Simple Reaction Machine

## Class Diagram

This diagram illustrates the main classes and interfaces involved in the Simple Reaction Machine, focusing on the State Design Pattern.

```mermaid
classDiagram
    direction LR

    class IController {
        <<interface>>
        +Connect(gui: IGui, rng: IRandom)
        +Init()
        +CoinInserted()
        +GoStopPressed()
        +Tick()
    }

    class IGui {
        <<interface>>
        +Connect(controller: IController)
        +Init()
        +SetDisplay(text: string)
    }

    class IRandom {
        <<interface>>
        +GetRandom(from: int, to: int): int
    }

    class SimpleReactionController {
        -IGui Gui
        -IRandom Rng
        -IReactionMachineState _currentState
        +Connect(gui: IGui, rng: IRandom)
        +Init()
        +CoinInserted()
        +GoStopPressed()
        +Tick()
        +SetState(newState: IReactionMachineState)
    }

    class IReactionMachineState {
        <<interface>>
        +Init()
        +CoinInserted()
        +GoStopPressed()
        +Tick()
    }

    class WaitingForCoinState {
        -SimpleReactionController _controller
        +WaitingForCoinState(controller: SimpleReactionController)
        +Init()
        +CoinInserted()
        +GoStopPressed()
        +Tick()
    }

    class WaitingForGoState {
        -SimpleReactionController _controller
        +WaitingForGoState(controller: SimpleReactionController)
        +Init()
        +CoinInserted()
        +GoStopPressed()
        +Tick()
    }

    class WaitingForReleaseState {
        -SimpleReactionController _controller
        -int _randomDelayTicks
        -int _currentTickCount
        +WaitingForReleaseState(controller: SimpleReactionController)
        +Init()
        +CoinInserted()
        +GoStopPressed()
        +Tick()
    }

    class TimingState {
        -SimpleReactionController _controller
        -int _reactionTimeTicks
        +TimingState(controller: SimpleReactionController)
        +Init()
        +CoinInserted()
        +GoStopPressed()
        +Tick()
        -FormatTime(ticks: int): string
    }

    class DisplayingReactionTimeState {
        -SimpleReactionController _controller
        -int _reactionTimeTicks
        -int _displayDurationTicks
        +DisplayingReactionTimeState(controller: SimpleReactionController)
        +Init()
        +CoinInserted()
        +GoStopPressed()
        +Tick()
        -FormatTime(ticks: int): string
    }

    SimpleReactionController --|> IController
    SimpleReactionController ..> IReactionMachineState : current state
    IReactionMachineState <|.. WaitingForCoinState
    IReactionMachineState <|.. WaitingForGoState
    IReactionMachineState <|.. WaitingForReleaseState
    IReactionMachineState <|.. TimingState
    IReactionMachineState <|.. DisplayingReactionTimeState

    SimpleReactionController ..> IGui : uses
    SimpleReactionController ..> IRandom : uses

    WaitingForCoinState o-- SimpleReactionController : aggregates
    WaitingForGoState o-- SimpleReactionController : aggregates
    WaitingForReleaseState o-- SimpleReactionController : aggregates
    TimingState o-- SimpleReactionController : aggregates
    DisplayingReactionTimeState o-- SimpleReactionController : aggregates
```

## State Diagram

This diagram visualizes the states and transitions of the Simple Reaction Machine's behavior.

```mermaid
stateDiagram-v2
    direction LR

    [*] --> WaitingForCoin

    WaitingForCoin --> WaitingForGo : CoinInserted()
    WaitingForCoin --> WaitingForCoin : Init() / Display "Insert coin"
    WaitingForCoin --> WaitingForCoin : GoStopPressed() (Invalid)
    WaitingForCoin --> WaitingForCoin : Tick() (No action)

    WaitingForGo --> WaitingForRelease : GoStopPressed() / Display "Wait..."
    WaitingForGo --> WaitingForGo : CoinInserted() (Invalid)
    WaitingForGo --> WaitingForGo : Tick() (No action)

    WaitingForRelease --> Timing : Tick() (Random Delay Expired)
    WaitingForRelease --> WaitingForCoin : GoStopPressed() (Cheating) / Display "Insert coin"
    WaitingForRelease --> WaitingForRelease : CoinInserted() (No action)

    Timing --> DisplayingReactionTime : GoStopPressed() / Record Time
    Timing --> DisplayingReactionTime : Tick() (2s Timeout) / Record Time 2.00
    Timing --> Timing : CoinInserted() (No action)

    DisplayingReactionTime --> WaitingForCoin : Tick() (3s Elapsed) / Display "Insert coin"
    DisplayingReactionTime --> WaitingForCoin : GoStopPressed() / Display "Insert coin"
    DisplayingReactionTime --> DisplayingReactionTime : CoinInserted() (No action)
```
