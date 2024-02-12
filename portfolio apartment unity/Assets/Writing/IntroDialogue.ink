-> END

... # rcr # anim_both_shocked # exclaim_left # hide_speaker # silent
... !! # jt # anim_both_shocked # hide_speaker # silent
...... # rcr # anim_both_shocked # hide_speaker # silent
WAIT!!! WHAT?? You just came crashing through my window! # jt # anim_both_shocked # exclaim_right # hide_speaker
Oh. Well, it wouldn't open from the outside.  # rcr # anim_left_shocked # hide_speaker
I think this was the next logical action actually! # rcr # anim_left_shocked # hide_speaker
....................... # jt # anim_left_idle # hide_speaker # speaker
AS I was saying, I'm a recruiter with a nose for talent! # rcr # hide_speaker
On my daily bagel run- (there's a pretty good place near here) # rcr # hide_speaker
\-my nose smelled a great deal of potential! # rcr # hide_speaker 

So, tell me about yourself! # rcr # rant # hide_speaker

The recruiter begins dusting off broken glass shards from- # narrator # anim_right_speaking # anim_left_shocked
\-the freshly baked bagel stashed in their pocket. # narrator # anim_right_speaking

Erm, my name is Justin and I use he/him pronouns. # jt # exclaim_left

Excellent, pleased to meet you. # rcr # hide_speaker
She grabs a glass-free bite out of her bagel and mumbles aloud. # narrator
You can call me Recruiter and I use she/her pronouns. # rcr 


Do go on! # rcr

Hm, and I actually happen to be looking for work right now. # jt

Perfect! How about we try this? # rcr

I have some questions to find out what makes you tick # rcr
If I want to ask them, I can left click on you. # rcr
Then, we'll have a good ol' fashioned dialogue about it. # rcr
If any of your projects laid out here catch my eye- # rcr
\they'll light up and I can left click to ask you about them. # rcr
Sound good? # rcr
Oddly specific but sounds professional. Let's do it! # jt
-> END


// rcr and jt correspond to who is speaking. 

// you can override the default animation states by specifying their name + the name of animation e.g. "jt_shocked" sets justin to the shocked animation and has higher priority over the default. 

// as a shortcut to set both to the same state, you can use the "both" prefix. e.g. "both_shocked" 

// some default states:
// - the speaker will by default use the speaking animation. you can override this by specifying "side_animationName" tag. for example, "Test sentence # justin # shocked_left" justin is hard coded to the left and shocked left applies the shocked animation to the left char slot. 
// - the listener will use the idle animation by default, you can override this similarly above 