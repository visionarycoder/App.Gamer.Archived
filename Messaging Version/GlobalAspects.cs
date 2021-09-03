// Turn on verbose logging
//#define LOGGING_ON

// This file contains registration of aspects that are applied to several classes of this project.
#if LOGGING_ON
[assembly: Log(AttributeTargetTypeAttributes = MulticastAttributes.Public | MulticastAttributes.Protected | MulticastAttributes.Private, AttributeTargetMemberAttributes = MulticastAttributes.Public | MulticastAttributes.Protected | MulticastAttributes.Private)]
#endif