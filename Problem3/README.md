# Exercise

Given the root to a binary tree, implement `serialize(root)`, which seriliazesthe tree into a string, and `deserialize(s)`, which deserializes the string back into the tree.

For example, given the following `Node` class:

```python3
class Node:
	def ___init___(self, val, left=None, right=None):
		self.val = val
		self.left = left
		self.right = right
```

The following test should pass:

```python3
node = Node('root', Node('left', Node('left.left')), Node('right'))
assert deserialize(serialize(node)).left.left.val == 'left.left'
```