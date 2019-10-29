# PubLab6
Lab 6 ITHS

TODO
Make the guests drink beer in order
	make a queue to put guests in after they entered (Blocking Collection)

(-1)clean glass -> guest drinks -> (+1)dirty glass(-1) -> waiter wash glass -> (+1) clean glasses 


guest add glasses to dirty glasses when leaving

concurrentBag.TryPeek to check if there is dirty glasses. pick up glasses if it returns true. wait time to not pick up 1 by 1

bartender, check if clean glasses. otherwise wait for clean glasses from waiter
