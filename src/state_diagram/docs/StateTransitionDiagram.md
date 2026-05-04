# State Transition Diagram for Simple Reaction Machine

## States:
- **WaitingForCoin**: The machine is idle, waiting for a coin to be inserted.
- **WaitingForGo**: A coin has been inserted, waiting for the player to press the Go/Stop button to start the game.
- **WaitingForRelease**: The game has started, waiting for a random delay to expire. The player should not press Go/Stop during this state.
- **Timing**: The random delay has expired, and the timer is incrementing, waiting for the player to press Go/Stop to record their reaction time.
- **DisplayingReactionTime**: The reaction time is displayed for a short period, or an error message if the player cheated or timed out.

## Transitions:

```mermaid
graph TD
    A[Start] --> WaitingForCoin;

    WaitingForCoin -- Init() / Display "Insert coin" --> WaitingForCoin;
    WaitingForCoin -- CoinInserted() / Display "Press GO!" --> WaitingForGo;
    WaitingForCoin -- GoStopPressed() / (Invalid input) --> WaitingForCoin;
    WaitingForCoin -- Tick() / (No action) --> WaitingForCoin;

    WaitingForGo -- CoinInserted() / (Invalid input) --> WaitingForGo;
    WaitingForGo -- GoStopPressed() / Display "Wait..." --> WaitingForRelease;
    WaitingForGo -- Tick() / (No action) --> WaitingForGo;

    WaitingForRelease -- GoStopPressed() (Cheating) / Display "Insert coin" --> WaitingForCoin;
    WaitingForRelease -- Tick() (Random Delay Expires) / (Implicitly starts timer at 0.00) --> Timing;
    WaitingForRelease -- CoinInserted() / (No action) --> WaitingForRelease;

    Timing -- GoStopPressed() / Record & Display Reaction Time --> DisplayingReactionTime;
    Timing -- Tick() (2 seconds timeout) / Record & Display 2.00 --> DisplayingReactionTime;
    Timing -- CoinInserted() / (No action) --> Timing;

    DisplayingReactionTime -- Tick() (3 seconds elapsed) / Display "Insert coin" --> WaitingForCoin;
    DisplayingReactionTime -- GoStopPressed() / Display "Insert coin" --> WaitingForCoin;
    DisplayingReactionTime -- CoinInserted() / (No action) --> DisplayingReactionTime;
```

## State Actions (Simplified):

- **WaitingForCoin:**
    - `Init()`: `_controller.Gui.SetDisplay("Insert coin");`
    - `CoinInserted()`: `_controller.SetState(new WaitingForGoState(_controller)); _controller.Gui.SetDisplay("Press GO!");`
- **WaitingForGo:**
    - `GoStopPressed()`: `_controller.SetState(new WaitingForReleaseState(_controller)); _controller.Gui.SetDisplay("Wait...");`
- **WaitingForRelease:**
    - `Constructor`: Generate `_randomDelayTicks`.
    - `GoStopPressed()`: `_controller.Gui.SetDisplay("Insert coin"); _controller.SetState(new WaitingForCoinState(_controller));` (Cheating)
    - `Tick()`: `_currentTickCount++`; if `_currentTickCount >= _randomDelayTicks`, `_controller.SetState(new TimingState(_controller));`
- **Timing:**
    - `Constructor`: `_reactionTimeTicks = 0; _controller.Gui.SetDisplay(FormatTime(_reactionTimeTicks));`
    - `GoStopPressed()`: `_controller.SetState(new DisplayingReactionTimeState(_controller, _reactionTimeTicks));`
    - `Tick()`: `_reactionTimeTicks++`; `_controller.Gui.SetDisplay(FormatTime(_reactionTimeTicks));` if `_reactionTimeTicks >= 200`, `_controller.SetState(new DisplayingReactionTimeState(_controller, _reactionTimeTicks));`
- **DisplayingReactionTime:**
    - `Constructor`: `_displayDurationTicks = 0; _controller.Gui.SetDisplay(FormatTime(_reactionTimeTicks));`
    - `GoStopPressed()`: `_controller.Gui.SetDisplay("Insert coin"); _controller.SetState(new WaitingForCoinState(_controller));`
    - `Tick()`: `_displayDurationTicks++`; if `_displayDurationTicks >= 300`, `_controller.Gui.SetDisplay("Insert coin"); _controller.SetState(new WaitingForCoinState(_controller));`
