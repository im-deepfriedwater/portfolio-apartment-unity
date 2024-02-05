... # rcr # anim_both_shocked # exclaim_left # hide_speaker
... !! # jt # anim_both_shocked # hide_speaker
...... # rcr # anim_both_shocked # hide_speaker
Well, that was exciting! Anyways, a pleasure. I'm- # rcr # anim_left_shocked # hide_speaker
WAIT!!! WHAT?? You just came crashing through my window- # jt # anim_both_shocked # exclaim_right # hide_speaker
Oh. Well, it wouldn't open from the outside.  # rcr # anim_left_shocked # hide_speaker # exclaim_left
I think this was the next most logical action. # rcr # anim_left_shocked # hide_speaker
... # jt # anim_left_idle # hide_speaker
AS I was saying, I'm a recruiter with a nose for professional potential! # rcr # hide_speaker
And on my daily bagel run- they run a really good shop over there I'll give you the address- my nose smelled a smell of great talent ! So, tell me about yourself! # rcr # rant # anim_left_shocked # hide_speaker

The recruiter begins dusting off broken glass shards from the fresh bagel that was in their pocket. # narrator # anim_right_speaking # anim_left_shocked # hide_speaker

Uh. Right. Erm, my name is Justin and I use pronouns he/him. # jt

Excellent, pleased to meet you. You can call me Recruiter and I use she/her pronouns. # rcr

She grabs a glass-free bite out of her bagel. She mumbles with a mouthful of bagel. # narrator

Do go on! # rcr

And well I do happen to be looking for work! # jt

Let's do this then. I see some interesting projects laid out around your apartment. How about I walk around the apartment using the <b> mouse </b> and <b> left click</b> to see what they're on about. And if I want to get to know more about you I'll <b> left click </b> on you and we can have a good ol' fashioned dialogue about it. # rcr

Very specific but I guess you're the professional! # jt


-> END


// rcr and jt correspond to who is speaking. 

// you can override the default animation states by specifying their name + the name of animation e.g. "jt_shocked" sets justin to the shocked animation and has higher priority over the default. 

// as a shortcut to set both to the same state, you can use the "both" prefix. e.g. "both_shocked" 

// some default states:
// - the speaker will by default use the speaking animation. you can override this by specifying "side_animationName" tag. for example, "Test sentence # justin # shocked_left" justin is hard coded to the left and shocked left applies the shocked animation to the left char slot. 
// - the listener will use the idle animation by default, you can override this similarly above 