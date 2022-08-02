let dice = [
    {
      name: 'W4',
      sides: 4,
      values: [],
      number: 0,
      modifier: 0,
      image: 'https://i.imgur.com/XyqQZQs.png'
    },
    {
      number: 0,
      name: 'W6',
      sides: 6,
      values: [],
      modifier: 0,
      image: 'https://i.imgur.com/XyqQZQs.png'
    },
    {
      number: 0,
      name: 'W8',
      sides: 8,
      values: [],
      modifier: 0,
      image: 'https://i.imgur.com/XyqQZQs.png'
    },
    {
      number: 0,
      name: 'W10',
      sides: 10,
      values: [],
      modifier: 0,
      image: 'https://i.imgur.com/XyqQZQs.png'
    },
    {
      number: 0,
      name: 'W12',
      sides: 12,
      values: [],
      modifier: 0,
      image: 'https://i.imgur.com/XyqQZQs.png'
    },
    {
      number: 0,
      name: 'W20',
      sides: 20,
      values: [],
      modifier: 0,
      image: 'https://i.imgur.com/XyqQZQs.png'
    },
    {
      number: 0,
      name: 'W100',
      sides: 100,
      values: [],
      modifier: 0,
      image: 'https://i.imgur.com/XyqQZQs.png'
    }
  ];

function roll2(selection)
{
    selection.forEach(d => 
        {
            d.values = [];
            d.total = 0;
            for (var i = 0; i < d.number; i++)             
            {
                const newValue = Math.floor(Math.random() * d.sides) + 1;
                d.values.push(newValue);
                d.total += newValue;
            }
                
        });
    return selection;
}

export { dice, roll2 }