import React, {useState } from 'react';


export const Counter = () => {
    const [count, setcount] = useState(0);

    const incrementCounter = () => {
        return setcount(count + 1);
    }
    return (
        <div>
            <h1>Counter</h1>

            <p>This is a simple example of a React component.</p>

            <p aria-live="polite">Current count: <strong>{count}</strong></p>

            <button className="btn btn-primary" onClick={incrementCounter}>Increment</button>
        </div>
        );
}

//export class Counter extends Component {
//  static displayName = Counter.name;

//  constructor(props) {
//    super(props);
//    this.state = { currentCount: 0 };
//    this.incrementCounter = this.incrementCounter.bind(this);
//  }

//  incrementCounter() {
//    this.setState({
//      currentCount: this.state.currentCount + 1
//    });
//  }

//  render() {
//    return (
//      <div>
//        <h1>Counter</h1>

//        <p>This is a simple example of a React component.</p>

//        <p aria-live="polite">Current count: <strong>{this.state.currentCount}</strong></p>

//        <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
//      </div>
//    );
//  }
//}
